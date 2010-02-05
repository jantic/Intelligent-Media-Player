
Partial Public Class MainInterface
    Partial Private Class GUIAsyncUpdater
        Private myArtistInfoUpdater As ArtistInfoGUIAsyncUpdater

        Private Delegate Sub AsyncSub()

        Public Sub New(ByRef theParentInterface As MainInterface)
            myArtistInfoUpdater = New ArtistInfoGUIAsyncUpdater(theParentInterface)
        End Sub

        Public Sub DisplayInfoForArtist(ByVal artistName As String)
            myArtistInfoUpdater.AddArtistNameToQueue(artistName)
        End Sub
    End Class
End Class