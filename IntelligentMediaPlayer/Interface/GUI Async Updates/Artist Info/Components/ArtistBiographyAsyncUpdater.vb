Partial Public Class MainInterface
    Partial Private Class GUIAsyncUpdater
        Partial Private Class ArtistInfoGUIAsyncUpdater
            Private Class ArtistBiographyAsyncUpdater

                Private myBiographyArtistInfoQueue As New Queue(Of IArtistInfo)
                Private UpdateBiographyASYNC As New AsyncSub(AddressOf UpdateBiography)
                Private sleeptime As UInteger = 200 ' ms
                Private myParentInterface As MainInterface

                Private Delegate Sub AsyncSub()

                Public Sub New(ByRef theParentInterface As MainInterface)
                    myParentInterface = theParentInterface
                    UpdateBiographyASYNC.BeginInvoke(Nothing, Nothing)
                End Sub

                Public Sub AddArtistInfoToQueue(ByRef artistInfo As IArtistInfo)
                    myBiographyArtistInfoQueue.Enqueue(artistInfo)
                End Sub

                Private Sub UpdateBiography()
                    While (True)
                        Dim artistInfo As IArtistInfo = Nothing

                        While (myBiographyArtistInfoQueue.Count > 0)  'don't need/want to update for artist info object that has already expired
                            artistInfo = myBiographyArtistInfoQueue.Dequeue
                        End While

                        If (Not artistInfo Is Nothing) Then
                            myParentInterface.BiographyLoadingPB.BringToFront()
                            myParentInterface.ArtistImageLoadingPB.BringToFront()

                            myParentInterface.ArtistPictureBox.ImageLocation = artistInfo.PictureLocation

                            If (myParentInterface.InvokeRequired()) Then
                                Dim d As New UpdateControlsWithArtistInfoDELEGATE(AddressOf UpdateControlsWithArtistInfo)
                                myParentInterface.Invoke(d, artistInfo)
                            Else
                                UpdateControlsWithArtistInfo(artistInfo)
                            End If

                            myParentInterface.FullBioTB.BringToFront()
                            myParentInterface.ArtistPictureBox.BringToFront()
                        End If

                        System.Threading.Thread.Sleep(sleeptime)
                    End While
                End Sub

                Private Delegate Sub UpdateControlsWithArtistInfoDELEGATE(ByRef artistInfo As IArtistInfo)

                Private Sub UpdateControlsWithArtistInfo(ByRef artistInfo As IArtistInfo)
                    Try
                        myParentInterface.FullBioTB.DocumentText = artistInfo.Biography
                    Catch ex As Exception
                        MsgBox(ex.Message) 'usually fails because of "too busy" error"- going to ignore it for now.
                    End Try

                    Try
                        If (Not myParentInterface.FullBioTB.Document.Body Is Nothing) Then
                            Dim zoomLevel As Integer = 10
                            myParentInterface.FullBioTB.Document.Body.Style = "zoom: " & zoomLevel.ToString & "%"
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message) 'not the end of the world if we just ignore it failing, for now.
                    End Try
                End Sub
            End Class
        End Class
    End Class
End Class

