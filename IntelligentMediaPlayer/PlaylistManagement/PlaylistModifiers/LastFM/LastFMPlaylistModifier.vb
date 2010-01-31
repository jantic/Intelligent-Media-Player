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


                Dim resultIndex As UInteger = 0

                While (1)

                    Dim attributeLookup As Dictionary(Of String, String) = New Dictionary(Of String, String)

                    For Each attributeMapping As LastFMOutputMapping In myCallResultMappings

                        Dim attributeName As String = attributeMapping.Attribute
                        Dim attributeValue As String = attributeMapping.ExtractOutputValue(resultIndex, myCallResultXML)

                        If (attributeValue Is Nothing) Then
                            Exit While
                        End If

                        attributeValue.Trim.ToLower()
                        attributeLookup.Add(attributeName, attributeValue)

                    Next


                    'add action code here.
                    Liason.ModifierAction.ModifyPlaylist(player, attributeLookup)

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
