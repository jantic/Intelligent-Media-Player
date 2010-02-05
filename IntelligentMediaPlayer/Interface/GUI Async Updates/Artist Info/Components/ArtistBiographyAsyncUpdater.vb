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

                            myParentInterface.ArtistPictureBox.ImageLocation = artistInfo.PictureLocation

                            Try
                                myParentInterface.FullBioTB.DocumentText = artistInfo.Biography
                            Catch
                                'usually fails because of "too busy" error"- going to ignore it for now.
                            End Try

                            Try

                                If (Not myParentInterface.FullBioTB.Document.Body Is Nothing) Then
                                    Dim zoomLevel As Integer = 10
                                    myParentInterface.FullBioTB.Document.Body.Style = "zoom: " & zoomLevel.ToString & "%"
                                End If
                            Catch
                                'not the end of the world if we just ignore it failing, for now.
                            End Try
                        End If
                        System.Threading.Thread.Sleep(sleeptime)
                    End While
                End Sub
            End Class
        End Class
    End Class
End Class

