
Partial Public Class PlaylistManager
    Private Interface IPlaylistModifier
        ReadOnly Property Liason() As PlaylistModifierUILiason
        ReadOnly Property ModificationAction() As IModifierAction
        Sub ModifyPlaylist(ByRef currentPlaylist As Playlist, ByRef mediaCollection As MediaCollection, Optional ByVal UseCachedResults As Boolean = False)
    End Interface
End Class