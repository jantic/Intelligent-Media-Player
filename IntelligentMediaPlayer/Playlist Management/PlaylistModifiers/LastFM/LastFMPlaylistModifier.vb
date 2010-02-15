Imports System.Xml
Imports System.Xml.XPath

Partial Public Class PlaylistManager

    Private Class LastFMPlaylistModifier
        Implements IPlaylistModifier

        Private myLiason As PlaylistModifierUILiason
        Private myURLTemplateMapping As URLTemplateMapping
        Private myCallResultMappings As LastFMOutputMapping()
        Private myCallResultXML As XmlDocument 'cached because otherwise last.fm will kick this program out for too many calls.
        Private myCachedPlaylist As ArrayList = New ArrayList()
        Private myMusicLibraryStats As New MusicLibraryStats()

        Public Sub New(ByVal theLiason As PlaylistModifierUILiason)
            myLiason = New PlaylistModifierUILiason(theLiason)
            myURLTemplateMapping = New URLTemplateMapping(myLiason)
            myCallResultMappings = LastFMOutputMapping.LoadResultMappings(myLiason)
            RetrieveAndStoreResultsFromWebservice()
        End Sub


        Public Sub ModifyPlaylist(ByRef currentPlaylist As Playlist, ByRef mediaCollection As MediaCollection, Optional ByVal UseCachedResults As Boolean = False) Implements IPlaylistModifier.ModifyPlaylist

            If (UseCachedResults And Not (myCachedPlaylist.Count = 0)) Then
                ApplyCachedPlaylist(currentPlaylist)
            Else


                Dim resultIndex As UInteger = 0
                Dim attributeLookupArray As New ArrayList()

                Dim artistsInCurrentPlaylist As HashSet(Of String) = LoadArtistsInCurrentPlaylistLookup(currentPlaylist)
                Dim albumsInCurrentPlaylist As HashSet(Of String) = LoadAlbumsInCurrentPlaylistLookup(currentPlaylist)

                While (True)

                    Dim attributeLookup As New Dictionary(Of String, String)

                    For Each attributeMapping As LastFMOutputMapping In myCallResultMappings

                        Dim attributeName As String = attributeMapping.Attribute
                        Dim attributeValue As String = attributeMapping.ExtractOutputValue(resultIndex, myCallResultXML)

                        If (attributeValue Is Nothing) Then
                            Exit While
                        End If

                        attributeValue.Trim.ToLower()
                        attributeLookup.Add(attributeName, attributeValue)
                    Next

                    If (ShouldAddToLookupArray(attributeLookup, artistsInCurrentPlaylist, albumsInCurrentPlaylist)) Then
                        attributeLookupArray.Add(attributeLookup)
                    End If
                    resultIndex += 1
                End While

                Dim lookupArray() As Dictionary(Of String, String) = attributeLookupArray.ToArray(GetType(Dictionary(Of String, String)))

                Liason.ModifierAction.ModifyPlaylist(currentPlaylist, mediaCollection, lookupArray)
                CacheThePlaylist(currentPlaylist)
            End If
        End Sub

        Private Function ShouldAddToLookupArray(ByRef attributeLookup As Dictionary(Of String, String), ByRef artistLookup As HashSet(Of String), _
                ByRef albumLookup As HashSet(Of String)) As Boolean

            'This is an ugly but very effective optimization for the subtraction and filtering operations.  The difference in my testing on 
            'subtraction filters:  20 seconds (new) versus 70 seconds, or 30% of the non-optimized time to process.

           If (myLiason.ModifierAction.Name.Trim.ToLower = "subtract" Or myLiason.ModifierAction.Name.Trim.ToLower = "filter") Then
                If (attributeLookup.Keys.Count = 1) Then
                    Dim key As String = attributeLookup.Keys(0)
                    If (key.Trim.ToLower = "author") Then
                        Return artistLookup.Contains(attributeLookup.Item(key).Trim.ToLower)
                    End If
                ElseIf (attributeLookup.Keys.Count = 2) Then
                    Dim key1 As String = attributeLookup.Keys(0)
                    Dim key2 As String = attributeLookup.Keys(1)

                    If (key1 = "AlbumID" And key2 = "Author") Then
                        Return ((artistLookup.Contains(attributeLookup.Item(key1).Trim.ToLower) And albumLookup.Contains(attributeLookup.Item(key2).Trim.ToLower)) Or _
                                (artistLookup.Contains(attributeLookup.Item(key2).Trim.ToLower) And albumLookup.Contains(attributeLookup.Item(key1).Trim.ToLower)))
                    End If
                End If
            End If

            Return True
        End Function

        Private Function LoadArtistsInCurrentPlaylistLookup(ByRef currentPlaylist As Playlist) As HashSet(Of String)
            Dim lookup As New HashSet(Of String)

            For index As Integer = 0 To currentPlaylist.count - 1 Step 1
                lookup.Add(currentPlaylist.Item(index).getItemInfo("Author").Trim.ToLower)
            Next

            Return lookup
        End Function

        Private Function LoadAlbumsInCurrentPlaylistLookup(ByRef currentPlaylist As Playlist) As HashSet(Of String)
            Dim lookup As New HashSet(Of String)

            For index As Integer = 0 To currentPlaylist.count - 1 Step 1
                lookup.Add(currentPlaylist.Item(index).getItemInfo("AlbumID").Trim.ToLower)
            Next

            Return lookup
        End Function


        Private Sub CacheThePlaylist(ByRef currentPlaylist As Playlist)
            myCachedPlaylist.Clear()

            For index As Integer = 0 To currentPlaylist.count - 1 Step 1
                myCachedPlaylist.Add(currentPlaylist.Item(index))
            Next
        End Sub

        Private Sub ApplyCachedPlaylist(ByRef currentPlaylist As Playlist)

            If (myCachedPlaylist.Count > 0) Then
                currentPlaylist.clear()
                For Each mediaItem As Media In myCachedPlaylist
                    currentPlaylist.appendItem(mediaItem)
                Next
            End If
        End Sub


        Public ReadOnly Property Liason As PlaylistModifierUILiason Implements IPlaylistModifier.Liason
            Get
                Return myLiason
            End Get
        End Property

        Public ReadOnly Property ModificationAction As IModifierAction Implements IPlaylistModifier.ModificationAction
            Get
                Return myLiason.ModifierAction
            End Get
        End Property

        Private Sub RetrieveAndStoreResultsFromWebservice()
            Dim url As String = myURLTemplateMapping.GetResultingURL(myLiason.Inputs)
            myCallResultXML = WebServiceClient.GetClient.RetrieveResult(url)
        End Sub

    End Class

End Class
