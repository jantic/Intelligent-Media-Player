
Partial Public Class MainInterface
    Private Class GUIAsyncUpdater
        Private myArtistNameQueue As New Queue(Of String)

        Private myBiographyArtistInfoQueue As New Queue(Of IArtistInfo)
        Private myTagsArtistInfoQueue As New Queue(Of IArtistInfo)
        Private mySimilarArtistInfoQueue As New Queue(Of IArtistInfo)
        Private myTopAlbumsArtistInfoQueue As New Queue(Of IArtistInfo)

        Dim sleeptime As UInteger = 250 ' ms


        Private myParentInterface As MainInterface

        Public Sub New(ByRef theParentInterface As MainInterface)
            myParentInterface = theParentInterface

            Dim UpdateArtistInfoTHREAD As New System.Threading.Thread(AddressOf UpdateArtistInfo)
            UpdateArtistInfoTHREAD.Start()
            Dim UpdateBiographyTHREAD As New System.Threading.Thread(AddressOf UpdateBiography)
            UpdateBiographyTHREAD.Start()
            Dim UpdateSimilarArtistsTHREAD As New System.Threading.Thread(AddressOf UpdateSimilarArtists)
            UpdateSimilarArtistsTHREAD.Start()
            Dim UpdateTagsTHREAD As New System.Threading.Thread(AddressOf UpdateTags)
            UpdateTagsTHREAD.Start()
            Dim UpdateTopAlbumsTHREAD As New System.Threading.Thread(AddressOf UpdateTopAlbums)
            UpdateTopAlbumsTHREAD.Start()
        End Sub

        Public Sub AddArtistNameToQueue(ByVal artistName As String)
            If (myArtistNameQueue.Count = 0) Then
                myArtistNameQueue.Enqueue(artistName)
            ElseIf (artistName.Trim.ToLower <> myArtistNameQueue.Peek().Trim.ToLower) Then
                myArtistNameQueue.Enqueue(artistName)
            End If
        End Sub

        Private Sub UpdateArtistInfo()
            While (True)
                Dim currentArtist As String = Nothing

                While (myArtistNameQueue.Count > 0) 'don't need/want to update for artist info object that has already expired
                    currentArtist = myArtistNameQueue.Dequeue()
                End While

                If (Not currentArtist Is Nothing) Then
                    Dim artistInfo As IArtistInfo = New LastFMArtistInfo(currentArtist)
                    myBiographyArtistInfoQueue.Enqueue(artistInfo)
                    myTagsArtistInfoQueue.Enqueue(artistInfo)
                    mySimilarArtistInfoQueue.Enqueue(artistInfo)
                    myTopAlbumsArtistInfoQueue.Enqueue(artistInfo)
                    System.GC.Collect()
                End If

                System.Threading.Thread.Sleep(sleeptime)
            End While
        End Sub

        Private Sub UpdateTopAlbums()
            While (True)
                Dim artistInfo As IArtistInfo = Nothing

                While (myTopAlbumsArtistInfoQueue.Count > 0)  'don't need/want to update for artist info object that has already expired
                    artistInfo = myTopAlbumsArtistInfoQueue.Dequeue
                End While

                If (Not artistInfo Is Nothing) Then
                    If (Not myParentInterface.TopAlbumsLV.LargeImageList Is Nothing) Then
                        For Each oldImage As Image In myParentInterface.TopAlbumsLV.LargeImageList.Images  'clean up images first!  Otherwise- we get out of memory exceptions.
                            oldImage.Dispose()
                        Next
                    End If

                    myParentInterface.TopAlbumsLV.Clear()

                    Dim albums As IAlbumInfo() = artistInfo.TopAlbums()

                    If (Not albums Is Nothing) Then

                        Dim albumImages As ImageList = New ImageList()
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



                        myParentInterface.TopAlbumsLV.LargeImageList = albumImages

                        For Each album As IAlbumInfo In albums
                            myParentInterface.TopAlbumsLV.Items.Add(album.Name, album.Name)
                        Next
                    End If
                End If
                System.Threading.Thread.Sleep(sleeptime)
            End While
        End Sub

        Private Function GetSimilarArtistsImagesSize() As Drawing.Size
            Dim imageWidth As UInteger = 150
            Dim imageHeight As UInteger = imageWidth
            Return New System.Drawing.Size(imageWidth, imageHeight)
        End Function

        Private Function GetDefaultArtistImage() As Image
            Return New Bitmap(GetTopAlbumImagesSize().Width, GetTopAlbumImagesSize.Height) ' just a blank bitmap
        End Function


        Private Function GetTopAlbumImagesSize() As Drawing.Size
            Dim imageWidth As UInteger = 90
            Dim imageHeight As UInteger = imageWidth
            Return New System.Drawing.Size(imageWidth, imageHeight)
        End Function

        Private Function GetDefaultAlbumImage() As Image
            Return New Bitmap(GetTopAlbumImagesSize().Width, GetTopAlbumImagesSize.Height) ' just a blank bitmap
        End Function

        Private Sub UpdateTags()
            While (True)
                Dim artistInfo As IArtistInfo = Nothing

                While (myTagsArtistInfoQueue.Count > 0)  'don't need/want to update for artist info object that has already expired
                    artistInfo = myTagsArtistInfoQueue.Dequeue
                End While

                If (Not artistInfo Is Nothing) Then

                    Dim tags As String() = artistInfo.Tags()



                    Dim tagLabels As ArrayList = New ArrayList()

                    tagLabels.Add(myParentInterface.Tag1)
                    tagLabels.Add(myParentInterface.Tag2)
                    tagLabels.Add(myParentInterface.Tag3)
                    tagLabels.Add(myParentInterface.Tag4)
                    tagLabels.Add(myParentInterface.Tag5)

                    Dim index As UInteger = 0

                    'initialize to be invisible
                    For Each tagLabel As Label In tagLabels
                        tagLabel.Visible = False
                    Next

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
                System.Threading.Thread.Sleep(sleeptime)
            End While
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


        Private Sub UpdateSimilarArtists()
            While (True)
                Dim artistInfo As IArtistInfo = Nothing

                While (mySimilarArtistInfoQueue.Count > 0)  'don't need/want to update for artist info object that has already expired
                    artistInfo = mySimilarArtistInfoQueue.Dequeue
                End While

                If (Not artistInfo Is Nothing) Then

                    If (Not myParentInterface.SimilarArtistsLV.LargeImageList Is Nothing) Then

                        For Each oldImage As Image In myParentInterface.SimilarArtistsLV.LargeImageList.Images  'clean up images first!  Otherwise- we get out of memory exceptions.
                            oldImage.Dispose()
                        Next
                    End If

                    myParentInterface.SimilarArtistsLV.Clear()

                    If (Not artistInfo Is Nothing) Then
                        Dim similarArtists As IArtistInfo() = artistInfo.SimilarArtists()

                        If (Not similarArtists Is Nothing) Then

                            Dim index As UInteger = 0


                            Dim artistImages As ImageList = New ImageList()
                            artistImages.ImageSize = GetSimilarArtistsImagesSize()
                            artistImages.ColorDepth = ColorDepth.Depth32Bit

                            'get album imagelist built first
                            For Each artist As IArtistInfo In similarArtists
                                Dim artistPicture As Image
                                If (Not (artist.PictureLocation Is Nothing Or artist.PictureLocation = "")) Then
                                    Try
                                        artistPicture = Image.FromFile(artist.PictureLocation)
                                    Catch
                                        artistPicture = GetDefaultArtistImage()
                                    End Try
                                Else
                                    artistPicture = GetDefaultArtistImage()
                                End If


                                artistImages.Images.Add(artist.Name, artistPicture)
                            Next


                            myParentInterface.SimilarArtistsLV.LargeImageList = artistImages

                            For Each artist As IArtistInfo In similarArtists
                                myParentInterface.SimilarArtistsLV.Items.Add(artist.Name, artist.Name)
                            Next
                        End If
                    End If
                End If
                System.Threading.Thread.Sleep(sleeptime)
            End While
        End Sub
    End Class
End Class