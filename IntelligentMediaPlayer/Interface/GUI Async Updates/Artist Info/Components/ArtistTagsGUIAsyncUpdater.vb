Partial Public Class MainInterface
    Partial Private Class GUIAsyncUpdater
        Partial Private Class ArtistInfoGUIAsyncUpdater
            Private Class ArtistTagsGUIAsyncUpdater
                Private myParentInterface As MainInterface
                Private sleeptime As UInteger = 200 ' ms
                Private myTagsArtistInfoQueue As New Queue(Of IArtistInfo)
                Private UpdateTagsASYNC As New AsyncSub(AddressOf UpdateTags)

                Public Sub New(ByRef theParentInterface As MainInterface)
                    myParentInterface = theParentInterface
                    UpdateTagsASYNC.BeginInvoke(Nothing, Nothing)
                End Sub

                Public Sub AddArtistInfoToQueue(ByRef artistInfo As IArtistInfo)
                    myTagsArtistInfoQueue.Enqueue(artistInfo)
                End Sub

                Private Sub ClearArtistTagsUI()
                    myParentInterface.ArtistTagsLV.Clear()
                End Sub

                Private Sub UpdateTags()
                    While (True)
                        Dim artistInfo As IArtistInfo = Nothing

                        While (myTagsArtistInfoQueue.Count > 0)  'don't need/want to update for artist info object that has already expired
                            artistInfo = myTagsArtistInfoQueue.Dequeue
                        End While

                        If (Not artistInfo Is Nothing) Then
                            ClearArtistTagsUI()
                            myParentInterface.TagsLoadingPB.BringToFront()
                            Dim tags As String() = artistInfo.Tags()
                            Dim index As UInteger = 0

                            If (myTagsArtistInfoQueue.Count = 0) Then 'don't waste time adding tags to UI if new artist has since been added.
                                If (Not tags Is Nothing) Then

                                    For Each tag As String In tags
                                        myParentInterface.ArtistTagsLV.Items.Add(tag)
                                    Next
                                End If
                            End If
                            myParentInterface.ArtistTagsLV.BringToFront()
                        End If
                        System.Threading.Thread.Sleep(sleeptime)
                    End While
                End Sub
            End Class
        End Class
    End Class
End Class