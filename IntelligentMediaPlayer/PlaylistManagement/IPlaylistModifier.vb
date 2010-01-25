Imports WMPLib
Imports AxWMPLib

Partial Public Class PlaylistManager
    Private Interface IPlaylistModifier
        ReadOnly Property Liason() As PlaylistModifierUILiason
        ReadOnly Property ModificationAction() As Action
        Sub ModifyPlaylist(ByRef player As AxWindowsMediaPlayer, Optional ByVal UseCachedResult As Boolean = False)

    End Interface
End Class