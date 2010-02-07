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


        Public Sub ModifyPlaylist(ByRef currentPlaylist As IWMPPlaylist, ByRef mediaCollection As IWMPMediaCollection2, Optional ByVal UseCachedResult As Boolean = False) Implements IPlaylistModifier.ModifyPlaylist

            If (UseCachedResult And Not (myCachedPlaylist.Count = 0)) Then
                ApplyCachedPlaylist(currentPlaylist)
            Else


                Dim resultIndex As UInteger = 0
                Dim attributeLookupArray As New ArrayList()

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

                    attributeLookupArray.Add(attributeLookup)
                    resultIndex += 1
                End While

                Dim lookupArray() As Dictionary(Of String, String) = attributeLookupArray.ToArray(GetType(Dictionary(Of String, String)))

                Liason.ModifierAction.ModifyPlaylist(currentPlaylist, mediaCollection, lookupArray)
                CacheThePlaylist(currentPlaylist)
            End If
        End Sub


        Private Sub CacheThePlaylist(ByRef currentPlaylist As IWMPPlaylist)
            myCachedPlaylist.Clear()

            For index As Integer = 0 To currentPlaylist.count - 1 Step 1
                myCachedPlaylist.Add(currentPlaylist.Item(index))
            Next
        End Sub

        Private Sub ApplyCachedPlaylist(ByRef currentPlaylist As IWMPPlaylist)

            If (myCachedPlaylist.Count > 0) Then
                currentPlaylist.clear()
                For Each mediaItem As IWMPMedia In myCachedPlaylist
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
