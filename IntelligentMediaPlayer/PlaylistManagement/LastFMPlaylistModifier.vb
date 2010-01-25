Imports WMPLib
Imports AxWMPLib
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

        Public Sub New(ByVal theLiason As PlaylistModifierUILiason)
            myLiason = New PlaylistModifierUILiason(theLiason)
            myURLTemplateMapping = New URLTemplateMapping(myLiason)
            myCallResultMappings = LastFMOutputMapping.LoadResultMappings(myLiason)
            RetrieveAndStoreResultsFromWebservice()
        End Sub


        Public Sub ModifyPlaylist(ByRef player As AxWindowsMediaPlayer, Optional ByVal UseCachedResult As Boolean = False) Implements IPlaylistModifier.ModifyPlaylist

            If (UseCachedResult And Not (myCachedPlaylist.Count = 0)) Then
                ApplyCachedPlaylist(player)
            Else

                Dim mc As WMPLib.IWMPMediaCollection2 = player.mediaCollection
                Dim resultIndex As UInteger = 0

                While (1)

                    Dim query As WMPLib.IWMPQuery = mc.createQuery()

                    For Each attributeMapping As LastFMOutputMapping In myCallResultMappings

                        Dim attributeName As String = attributeMapping.Attribute
                        Dim attributeValue As String = attributeMapping.ExtractOutputValue(resultIndex, myCallResultXML)

                        If (attributeValue Is Nothing) Then
                            Exit While
                        End If

                        attributeValue.Trim.ToLower()

                        ' Add two conditions to the Query. 
                        query.addCondition(attributeName, "Equals", attributeValue)
                    Next

                    Dim result As IWMPPlaylist = mc.getPlaylistByQuery(query, "audio", "", False)

                    Dim quickMediaLookup As Dictionary(Of String, IWMPMedia) = New Dictionary(Of String, IWMPMedia) 'for proper, fast subtraction
                    'this makes subtraction a O(N) operation as opposed to an O(N2) operation

                    If (myLiason.ModifierAction = Action.Subtract) Then

                        For y As Integer = 0 To player.currentPlaylist.count - 1 Step 1

                            If (y >= player.currentPlaylist.count) Then 'need to check since we're potentially removing items.
                                Exit For
                            End If

                            Dim item As IWMPMedia = player.currentPlaylist.Item(y)
                            Dim key As String = GenerateMediaHashKey(item)

                            If (Not (quickMediaLookup.ContainsKey(key))) Then
                                quickMediaLookup.Add(key, item)
                            Else
                                player.currentPlaylist.removeItem(item)
                                y -= 1
                            End If
                        Next
                    End If



                    For index As Integer = 0 To result.count - 1 Step 1
                        Dim mediaItem As IWMPMedia = result.Item(index)

                        If (myLiason.ModifierAction = Action.Subtract) Then
                            Dim key As String = GenerateMediaHashKey(mediaItem)
                            If (quickMediaLookup.ContainsKey(key)) Then
                                player.currentPlaylist.removeItem(quickMediaLookup(GenerateMediaHashKey(mediaItem)))
                                quickMediaLookup.Remove(key)
                            End If
                        ElseIf (myLiason.ModifierAction = Action.Add) Then
                            player.currentPlaylist.appendItem(result.Item(index))
                        End If
                    Next


                    resultIndex += 1
                End While

                CacheThePlaylist(player)
            End If
        End Sub


        Private Sub CacheThePlaylist(ByRef player As AxWindowsMediaPlayer)
            myCachedPlaylist.Clear()

            For index As Integer = 0 To player.currentPlaylist.count - 1 Step 1
                myCachedPlaylist.Add(player.currentPlaylist.Item(index))
            Next
        End Sub

        Private Sub ApplyCachedPlaylist(ByRef player As AxWindowsMediaPlayer)

            If (myCachedPlaylist.Count > 0) Then
                player.currentPlaylist.clear()
                For Each mediaItem As IWMPMedia In myCachedPlaylist
                    player.currentPlaylist.appendItem(mediaItem)
                Next
            End If
        End Sub


        Public ReadOnly Property Liason As PlaylistModifierUILiason Implements IPlaylistModifier.Liason
            Get
                Return myLiason
            End Get
        End Property

        Public ReadOnly Property ModificationAction As Action Implements IPlaylistModifier.ModificationAction
            Get
                Return myLiason.ModifierAction
            End Get
        End Property

        Private Sub RetrieveAndStoreResultsFromWebservice()
            Dim url As String = myURLTemplateMapping.GetResultingURL(myLiason.Inputs)
            myCallResultXML = New XmlDocument()
            myCallResultXML.Load(url)
        End Sub

    End Class

End Class
