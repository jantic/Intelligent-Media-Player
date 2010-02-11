
Partial Public Class MainInterface
    Partial Private Class GUIAsyncUpdater
        Partial Private Class ArtistInfoGUIAsyncUpdater
            Private Class SimilarArtistsGUIAsyncUpdater
                Private mySimilarArtistInfoQueue As New Queue(Of IArtistInfo)
                Private UpdateSimilarArtistsASYNC As New AsyncSub(AddressOf UpdateSimilarArtists)
                Private myParentInterface As MainInterface
                Private sleeptime As UInteger = 200 ' ms
                Private myMusicLibraryStats As New MusicLibraryStats()

                Public Sub New(ByRef ParentInterface As MainInterface)
                    myParentInterface = ParentInterface
                    UpdateSimilarArtistsASYNC.BeginInvoke(Nothing, Nothing)
                End Sub

                Public Sub AddArtistInfoToQueue(ByRef artistInfo As IArtistInfo)
                    mySimilarArtistInfoQueue.Enqueue(artistInfo)
                End Sub

                Private Function GetSimilarArtistsImagesSize() As Drawing.Size
                    Dim imageWidth As UInteger = 90
                    Dim imageHeight As UInteger = imageWidth
                    Return New System.Drawing.Size(imageWidth, imageHeight)
                End Function

                Private Function GetDefaultArtistImage() As Image
                    Return New Bitmap(GetSimilarArtistsImagesSize().Width, GetSimilarArtistsImagesSize().Height) ' just a blank bitmap
                End Function

                Private Sub ClearSimilarArtistsUI()
                    If (Not myParentInterface.SimilarArtistsLV.LargeImageList Is Nothing) Then

                        For Each oldImage As Image In myParentInterface.SimilarArtistsLV.LargeImageList.Images  'clean up images first!  Otherwise- we get out of memory exceptions.
                            oldImage.Dispose()
                        Next
                    End If

                    myParentInterface.SimilarArtistsLV.Clear()
                End Sub

                Private Function GetSimilarArtistImages(ByVal similarArtists As IArtistNameFace())
                    Dim artistImages As ImageList = Nothing
                    If (Not similarArtists Is Nothing) Then

                        Dim index As UInteger = 0
                        artistImages = New ImageList()
                        artistImages.ImageSize = GetSimilarArtistsImagesSize()
                        artistImages.ColorDepth = ColorDepth.Depth32Bit

                        'get album imagelist built first
                        For Each artist As IArtistNameFace In similarArtists
                            Dim artistPicture As Image
                            If (Not (artist.LargePictureLocation Is Nothing Or artist.LargePictureLocation = "")) Then
                                Try
                                    artistPicture = Image.FromFile(artist.LargePictureLocation)
                                Catch
                                    artistPicture = GetDefaultArtistImage()
                                End Try
                            Else
                                artistPicture = GetDefaultArtistImage()
                            End If


                            artistImages.Images.Add(artist.Name, artistPicture)
                        Next
                    End If

                    Return artistImages
                End Function

                Private Sub UpdateSimilarArtists()
                    While (True)
                        Dim artistInfo As IArtistInfo = Nothing

                        While (mySimilarArtistInfoQueue.Count > 0)  'don't need/want to update for artist info object that has already expired
                            artistInfo = mySimilarArtistInfoQueue.Dequeue
                        End While

                        If (Not artistInfo Is Nothing) Then

                            ClearSimilarArtistsUI()

                            If (Not artistInfo Is Nothing) Then
                                Dim similarArtists As IArtistNameFace() = artistInfo.SimilarArtists()

                                If (Not similarArtists Is Nothing) Then

                                    Dim index As UInteger = 0
                                    Dim artistImages As ImageList = GetSimilarArtistImages(similarArtists)
                                    myParentInterface.SimilarArtistsLV.LargeImageList = artistImages

                                    For Each artist As IArtistNameFace In similarArtists
                                        Dim d As New HowManyTracksByArtistsDELEGATE(AddressOf myMusicLibraryStats.HowManyTracksByArtist)
                                        Dim count As UInteger = myParentInterface.Invoke(d, artist.Name)
                                        Dim textColor As Color = GetSimilarArtistTextColor(count)
                                        myParentInterface.SimilarArtistsLV.Items.Add(artist.Name, artist.Name).ForeColor = textColor
                                    Next
                                End If
                            End If
                        End If
                        System.Threading.Thread.Sleep(sleeptime)
                    End While
                End Sub

                Delegate Function HowManyTracksByArtistsDELEGATE(ByVal artist As String) As UInteger  'needed for thread safety


                Private Function GetSimilarArtistTextColor(ByVal numberOfTracks As UInteger)
                    If (numberOfTracks > 0) Then
                        Return Color.Blue
                    Else
                        Return Color.Black
                    End If
                End Function

            End Class
        End Class
    End Class
End Class
