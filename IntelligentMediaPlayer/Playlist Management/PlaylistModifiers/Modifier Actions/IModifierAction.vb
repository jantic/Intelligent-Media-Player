Imports AxWMPLib
Imports WMPLib

Partial Public Class PlaylistManager

    Public Interface IModifierAction
        Sub ModifyPlaylist(ByRef currentPlaylist As IWMPPlaylist, ByRef mediaCollection As IWMPMediaCollection2, ByRef attributeLookupArray() As Dictionary(Of String, String))
        Sub ModifyPlaylist(ByRef currentPlaylist As IWMPPlaylist, ByRef mediaCollection As IWMPMediaCollection2, ByRef modifyingPlaylist As IWMPPlaylist)
        ReadOnly Property Name() As String
    End Interface

End Class
