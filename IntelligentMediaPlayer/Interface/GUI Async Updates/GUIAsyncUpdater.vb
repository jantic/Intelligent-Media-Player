
Partial Public Class MainInterface
    Partial Private Class GUIAsyncUpdater
        Private myArtistInfoUpdater As ArtistInfoGUIAsyncUpdater
        Private myAlbumInfoUpdater As AlbumInfoGUIAsyncUpdater

        Private Delegate Sub AsyncSub()

        Public Sub New(ByRef theParentInterface As MainInterface)
            myArtistInfoUpdater = New ArtistInfoGUIAsyncUpdater(theParentInterface)
            myAlbumInfoUpdater = New AlbumInfoGUIAsyncUpdater(theParentInterface)
        End Sub

        Public Sub DisplayInfoForArtist(ByVal artistName As String)
            myArtistInfoUpdater.AddArtistNameToQueue(artistName)
        End Sub

        Public Sub DisplayInfoForAlbum(ByVal artistName As String, ByVal albumName As String)
            myAlbumInfoUpdater.AddAlbumToQueue(albumName, artistName)
        End Sub

    End Class
End Class