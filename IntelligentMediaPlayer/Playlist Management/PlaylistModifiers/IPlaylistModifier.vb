Imports WMPLib
Imports AxWMPLib

Partial Public Class PlaylistManager
    Private Interface IPlaylistModifier
        ReadOnly Property Liason() As PlaylistModifierUILiason
        ReadOnly Property ModificationAction() As IModifierAction
        Sub ModifyPlaylist(ByRef currentPlaylist As IWMPPlaylist, ByRef mediaCollection As IWMPMediaCollection2, Optional ByVal UseCachedResults As Boolean = False)

    End Interface
End Class