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
                    Dim tagLabels As ArrayList = New ArrayList()

                    tagLabels.Add(myParentInterface.Tag1)
                    tagLabels.Add(myParentInterface.Tag2)
                    tagLabels.Add(myParentInterface.Tag3)
                    tagLabels.Add(myParentInterface.Tag4)
                    tagLabels.Add(myParentInterface.Tag5)

                    Dim index As UInteger = 0

                    'initialize to be invisible
                    For Each tagLabel As Label In tagLabels
                        tagLabel.Text = ""
                        tagLabel.Visible = False
                    Next
                End Sub

                Private Sub UpdateTags()
                    While (True)
                        Dim artistInfo As IArtistInfo = Nothing

                        While (myTagsArtistInfoQueue.Count > 0)  'don't need/want to update for artist info object that has already expired
                            artistInfo = myTagsArtistInfoQueue.Dequeue
                        End While

                        If (Not artistInfo Is Nothing) Then
                            ClearArtistTagsUI()
                            Dim tags As String() = artistInfo.Tags()

                            Dim tagLabels As ArrayList = New ArrayList()

                            tagLabels.Add(myParentInterface.Tag1)
                            tagLabels.Add(myParentInterface.Tag2)
                            tagLabels.Add(myParentInterface.Tag3)
                            tagLabels.Add(myParentInterface.Tag4)
                            tagLabels.Add(myParentInterface.Tag5)

                            Dim index As UInteger = 0

                            If (myTagsArtistInfoQueue.Count = 0) Then 'don't waste time adding tags to UI if new artist has since been added.


                                If (Not tags Is Nothing) Then

                                    For Each tagLabel As Label In tagLabels
                                        If (index >= tags.Count) Then
                                            Exit For
                                        End If

                                        tagLabel.Text = tags(index)
                                        tagLabel.Visible = True
                                        index += 1
                                    Next
                                End If
                            End If
                        End If
                        System.Threading.Thread.Sleep(sleeptime)
                    End While
                End Sub
            End Class
        End Class
    End Class
End Class