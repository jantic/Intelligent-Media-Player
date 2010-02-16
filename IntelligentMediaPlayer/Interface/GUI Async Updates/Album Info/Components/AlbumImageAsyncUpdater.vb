Partial Public Class MainInterface
    Partial Private Class GUIAsyncUpdater
        Partial Private Class AlbumInfoGUIAsyncUpdater

            Private Class AlbumImageAsyncUpdater
                Private myAlbumInfoQueue As New Queue(Of IAlbumInfo)
                Private UpdateAlbumImageASYNC As New AsyncSub(AddressOf UpdateAlbumImage)
                Private sleeptime As UInteger = 200 ' ms
                Private myParentInterface As MainInterface

                Private Delegate Sub AsyncSub()

                Public Sub New(ByRef theParentInterface As MainInterface)
                    myParentInterface = theParentInterface
                    UpdateAlbumImageASYNC.BeginInvoke(Nothing, Nothing)
                End Sub

                Public Sub AddAlbumInfoToQueue(ByRef albumInfo As IAlbumInfo)
                    myAlbumInfoQueue.Enqueue(albumInfo)
                End Sub

                Private Sub UpdateAlbumImage()
                    While (True)
                        Dim albumInfo As IAlbumInfo = Nothing

                        While (myAlbumInfoQueue.Count > 0)  'don't need/want to update for artist info object that has already expired
                            albumInfo = myAlbumInfoQueue.Dequeue
                        End While

                        If (Not albumInfo Is Nothing) Then
                            myParentInterface.AlbumImageLoadingPB.BringToFront()
                            myParentInterface.AlbumPictureBox.ImageLocation = albumInfo.LargePictureLocation
                            myParentInterface.AlbumPictureBox.BringToFront()
                        End If
                        System.Threading.Thread.Sleep(sleeptime)
                    End While
                End Sub

            End Class
        End Class
    End Class
End Class
