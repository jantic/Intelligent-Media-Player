
Partial Public Class MainInterface
    Partial Private Class GUIAsyncUpdater
        Partial Private Class ArtistInfoGUIAsyncUpdater
            Private Class TopAlbumsGUIAsyncUpdater
                Private myTopAlbumsArtistInfoQueue As New Queue(Of IArtistInfo)
                Private UpdateTopAlbumsASYNC As New AsyncSub(AddressOf UpdateTopAlbums)
                Private sleeptime As UInteger = 200 ' ms
                Private myParentInterface As MainInterface
                Private myMusicLibraryStats As New MusicLibraryStats()

                Public Sub New(ByRef ParentInterface As MainInterface)
                    myParentInterface = ParentInterface
                    UpdateTopAlbumsASYNC.BeginInvoke(Nothing, Nothing)
                End Sub

                Public Sub AddArtistInfoToQueue(ByRef artistInfo As IArtistInfo)
                    myTopAlbumsArtistInfoQueue.Enqueue(artistInfo)
                End Sub

                Private Sub ClearTopAlbumsUI()
                    If (Not myParentInterface.TopAlbumsLV.LargeImageList Is Nothing) Then
                        For Each oldImage As Image In myParentInterface.TopAlbumsLV.LargeImageList.Images  'clean up images first!  Otherwise- we get out of memory exceptions.
                            oldImage.Dispose()
                        Next
                    End If

                    myParentInterface.TopAlbumsLV.Clear()
                End Sub

                Private Function RetrieveTopAlbumImages(ByVal albums As IAlbumInfo())
                    Dim albumImages As ImageList = Nothing

                    If (Not albums Is Nothing) Then

                        albumImages = New ImageList()
                        albumImages.ImageSize = GetTopAlbumImagesSize()
                        albumImages.ColorDepth = ColorDepth.Depth32Bit
                        'get album imagelist built first
                        For Each album As IAlbumInfo In albums
                            Dim albumArt As Image
                            If (Not (album.PictureLocation Is Nothing Or album.PictureLocation = "")) Then
                                Try
                                    albumArt = Image.FromFile(album.PictureLocation)
                                Catch ex As Exception
                                    albumArt = GetDefaultAlbumImage()
                                End Try
                            Else
                                albumArt = GetDefaultAlbumImage()
                            End If

                            albumImages.Images.Add(album.Name, albumArt)
                        Next
                    End If

                    Return albumImages
                End Function

                Private Sub UpdateTopAlbums()
                    While (True)
                        Dim artistInfo As IArtistInfo = Nothing

                        While (myTopAlbumsArtistInfoQueue.Count > 0)  'don't need/want to update for artist info object that has already expired
                            artistInfo = myTopAlbumsArtistInfoQueue.Dequeue
                        End While

                        If (Not artistInfo Is Nothing) Then

                            ClearTopAlbumsUI()

                            Dim albums As IAlbumInfo() = artistInfo.TopAlbums()

                            If (Not albums Is Nothing) Then

                                Dim albumImages As ImageList = RetrieveTopAlbumImages(albums)

                                If (myTopAlbumsArtistInfoQueue.Count = 0) Then ' don't waste time adding images for albums if a new artist has been added.

                                    myParentInterface.TopAlbumsLV.LargeImageList = albumImages

                                    For Each album As IAlbumInfo In albums
                                        Dim artist As String = album.Artist.Name
                                        Dim albumName As String = album.Name
                                        Dim d As New DoIHaveThisAlbumDELEGATE(AddressOf myMusicLibraryStats.DoIHaveThisAlbum)
                                        Dim haveAlbum As Boolean = myParentInterface.Invoke(d, artist, albumName) ' for thread safety
                                        Dim textColor As Color = GetTopAlbumsTextColor(haveAlbum)
                                        myParentInterface.TopAlbumsLV.Items.Add(album.Name, album.Name).ForeColor = textColor
                                    Next
                                End If
                            End If
                        End If
                        System.Threading.Thread.Sleep(sleeptime)
                    End While
                End Sub

                Private Delegate Function DoIHaveThisAlbumDELEGATE(ByVal artist As String, ByVal albumName As String) As Boolean

                Private Function GetTopAlbumImagesSize() As Drawing.Size
                    Dim imageWidth As UInteger = 90
                    Dim imageHeight As UInteger = imageWidth
                    Return New System.Drawing.Size(imageWidth, imageHeight)
                End Function

                Private Function GetDefaultAlbumImage() As Image
                    Return New Bitmap(GetTopAlbumImagesSize().Width, GetTopAlbumImagesSize.Height) ' just a blank bitmap
                End Function

                Private Function GetTopAlbumsTextColor(ByVal haveAlbum As Boolean)
                    If (haveAlbum) Then
                        Return Color.Blue
                    Else
                        Return Color.Black
                    End If
                End Function
            End Class
        End Class
    End Class
End Class