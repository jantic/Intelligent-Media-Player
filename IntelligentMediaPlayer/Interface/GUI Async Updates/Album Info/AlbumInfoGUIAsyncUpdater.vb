Partial Public Class MainInterface
    Partial Private Class GUIAsyncUpdater

        Private Class AlbumInfoGUIAsyncUpdater
            Private sleeptime As UInteger = 200 ' ms
            Private myParentInterface As MainInterface
            Private myAlbumQueue As New Queue(Of String)
            Private UpdateAlbumInfoASYNC As New AsyncSub(AddressOf UpdateAlbumInfo)
            Private previousAlbum As String = ""

            Private myAlbumImageUpdater As AlbumImageAsyncUpdater


            Public Sub New(ByRef theParentInterface As MainInterface)
                myParentInterface = theParentInterface
                myAlbumImageUpdater = New AlbumImageAsyncUpdater(theParentInterface)
                UpdateAlbumInfoASYNC.BeginInvoke(Nothing, Nothing)
            End Sub

            Public Sub AddAlbumToQueue(ByVal albumName As String, ByVal artistName As String)
                If (albumName.Trim.ToLower <> previousAlbum.Trim.ToLower) Then
                    myParentInterface.AlbumImageLoadingPB.BringToFront()
                    myAlbumQueue.Enqueue(artistName + ";" + albumName)
                End If

                previousAlbum = albumName
            End Sub

            Private Sub UpdateAlbumInfo()
                While (True)
                    Dim currentAlbumArtistPair As String = Nothing

                    While (myAlbumQueue.Count > 0) 'don't need/want to update for artist info object that has already expired
                        currentAlbumArtistPair = myAlbumQueue.Dequeue()
                    End While

                    If (Not currentAlbumArtistPair Is Nothing) Then
                        If (currentAlbumArtistPair.Split(";").Count = 2) Then
                            Dim artistName As String = currentAlbumArtistPair.Split(";").ElementAt(0)
                            Dim albumName As String = currentAlbumArtistPair.Split(";").ElementAt(1)
                            Dim albumInfo As IAlbumInfo = New LastFMAlbumInfo(artistName, albumName)
                            If (myAlbumQueue.Count = 0) Then 'making sure we don't waste time updating anything if a new artist has been added.
                                myAlbumImageUpdater.AddAlbumInfoToQueue(albumInfo)
                                'System.GC.Collect()
                            End If
                        End If

                    End If

                    System.Threading.Thread.Sleep(sleeptime)
                End While
            End Sub
        End Class
    End Class
End Class
