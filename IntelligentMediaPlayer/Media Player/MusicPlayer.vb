Imports AxWMPLib
Imports WMPLib

'a wrapper class of windows media player

Public Class MusicPlayer
    Private myCurrentPlaylist As New Playlist
    Private myMediaCollection As MediaCollection = MediaCollection.GetMediaCollection()
    Private myPlayer As AxWindowsMediaPlayer
    Private myCurrentSelectedIndex As Integer = 0
    Private myControls As PlayerControls

    Public Sub New(ByRef player As AxWindowsMediaPlayer)
        myPlayer = player
        myControls = New PlayerControls(Me)
    End Sub


    Public Property currentMedia As Media
        Get
            If (CurrentMediaIndex >= 0) Then
                Return myCurrentPlaylist.Item(CurrentMediaIndex)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Media)
            myPlayer.currentMedia = value.GetMyWrappedMediaItem
        End Set
    End Property

    Public Property currentPlaylist As Playlist
        Get
            Return myCurrentPlaylist
        End Get
        Set(ByVal value As Playlist)
            myCurrentPlaylist = value

            If (Not myCurrentPlaylist Is Nothing) Then
                If (myCurrentPlaylist.count > 0) Then
                    UpdateInteralPlayerPlaylist()
                End If
            End If
        End Set
    End Property

    Public ReadOnly Property CurrentMediaIndex As Integer
        Get
            Return myPlayer.currentMedia.getItemInfo("PlaylistIndex")
        End Get
    End Property

    Private Sub UpdateInteralPlayerPlaylist()
        myPlayer.currentPlaylist.clear()

        For index As Integer = 0 To myCurrentPlaylist.count - 1 Step 1
            Dim currentMedia As Media = myCurrentPlaylist.Item(index)
            myPlayer.currentPlaylist.appendItem(currentMedia.GetMyWrappedMediaItem())
        Next
    End Sub


    Public ReadOnly Property MediaLibrary As MediaCollection
        Get
            Return myMediaCollection
        End Get
    End Property



    Public ReadOnly Property controls As PlayerControls
        Get
            Return myControls
        End Get
    End Property

    Public Sub ToggleShuffle(ByVal shuffle As Boolean)
        myPlayer.settings.setMode("shuffle", shuffle)
    End Sub


End Class
