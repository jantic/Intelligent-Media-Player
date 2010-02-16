Partial Public Class MainInterface
    Partial Private Class GUIAsyncUpdater
        Partial Private Class ArtistInfoGUIAsyncUpdater
            Private sleeptime As UInteger = 200 ' ms
            Private myParentInterface As MainInterface
            Private myArtistNameQueue As New Queue(Of String)
            Private UpdateArtistInfoASYNC As New AsyncSub(AddressOf UpdateArtistInfo)
            Private previousArtist As String = ""

            Private myBiographyUpdater As ArtistBiographyAsyncUpdater
            Private myTagsUpdater As ArtistTagsGUIAsyncUpdater
            Private mySimilarArtistsUpdater As SimilarArtistsGUIAsyncUpdater
            Private myTopAlbumsUpdater As TopAlbumsGUIAsyncUpdater

            Public Sub New(ByRef theParentInterface As MainInterface)
                myParentInterface = theParentInterface
                myBiographyUpdater = New ArtistBiographyAsyncUpdater(theParentInterface)
                myTagsUpdater = New ArtistTagsGUIAsyncUpdater(theParentInterface)
                mySimilarArtistsUpdater = New SimilarArtistsGUIAsyncUpdater(theParentInterface)
                myTopAlbumsUpdater = New TopAlbumsGUIAsyncUpdater(theParentInterface)
                UpdateArtistInfoASYNC.BeginInvoke(Nothing, Nothing)
            End Sub

            Public Sub AddArtistNameToQueue(ByVal artistName As String)
                If (artistName.Trim.ToLower <> previousArtist.Trim.ToLower) Then
                    myParentInterface.BiographyLoadingPB.BringToFront()
                    myParentInterface.ArtistImageLoadingPB.BringToFront()
                    myParentInterface.TopAlbumsLoadingPB.BringToFront()
                    myParentInterface.SimilarArtistsLoadingPB.BringToFront()
                    myParentInterface.TagsLoadingPB.BringToFront()

                    myArtistNameQueue.Enqueue(artistName)
                End If

                previousArtist = artistName
            End Sub

            Private Sub UpdateArtistInfo()
                While (True)
                    Dim currentArtist As String = Nothing

                    While (myArtistNameQueue.Count > 0) 'don't need/want to update for artist info object that has already expired
                        currentArtist = myArtistNameQueue.Dequeue()
                    End While

                    If (Not currentArtist Is Nothing) Then
                        Dim artistInfo As IArtistInfo = New LastFMArtistInfo(currentArtist)
                        If (myArtistNameQueue.Count = 0) Then 'making sure we don't waste time updating anything if a new artist has been added.
                            myBiographyUpdater.AddArtistInfoToQueue(artistInfo)
                            myTagsUpdater.AddArtistInfoToQueue(artistInfo)
                            mySimilarArtistsUpdater.AddArtistInfoToQueue(artistInfo)
                            myTopAlbumsUpdater.AddArtistInfoToQueue(artistInfo)
                            System.GC.Collect()
                        End If
                    End If

                    System.Threading.Thread.Sleep(sleeptime)
                End While
            End Sub
        End Class
    End Class
End Class
