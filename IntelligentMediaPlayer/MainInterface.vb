Imports WMPLib
Imports AxWMPLib

Public Class MainInterface
    Private client As LastFMClient = New LastFMClient

    Private player As AxWindowsMediaPlayer

    Private Sub Initialize() Handles Me.Load
        InitializePlayer()
        InitializePlaylist()
    End Sub

    Private Sub InitializePlayer()
        player = AxWindowsMediaPlayer1
    End Sub

    Private Sub InitializePlaylist()
        player.currentPlaylist.clear()
        player.currentPlaylist = player.mediaCollection.getByAttribute("MediaType", "audio")
        FillPlaylistBox()
    End Sub

    Private Sub FillPlaylistBox()
        PlaylistBox.Items.Clear()
        Dim count As Integer = player.currentPlaylist.count

        For index As Integer = 0 To count - 1 Step 1
            Dim media As IWMPMedia = player.currentPlaylist.Item(index)
            Dim trackName As String = media.getItemInfo("Author") + " - " + media.name
            PlaylistBox.Items.Add(trackName)
        Next
    End Sub

    Private Sub PlaylistBox_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlaylistBox.DoubleClick
        Dim selectedSong As IWMPMedia = player.currentPlaylist.Item(PlaylistBox.SelectedIndex)

        Try
            player.Ctlcontrols.playItem(player.currentPlaylist.Item(PlaylistBox.SelectedIndex))
        Catch ex As Exception
            MsgBox(ex.Message())
        End Try
    End Sub

    Private Sub FilterPlayList(ByRef artistList As Artist())
        player.Ctlcontrols.stop()
        player.currentPlaylist.clear()
        'leaving shuffle on during this process slows things down big time.  
        player.settings.setMode("shuffle", False)

        For Each item As Artist In artistList
            Dim tempPlayList As IWMPPlaylist = player.mediaCollection.getByAuthor(item.Name)
            Dim count As Integer = tempPlayList.count()

            For index As Integer = 0 To count - 1 Step 1
                player.currentPlaylist.appendItem(tempPlayList.Item(index))
            Next
        Next

        ToggleShuffle()
        FillPlaylistBox()
    End Sub


    Private Sub FilterPlayList(ByRef trackList As Track())
        player.Ctlcontrols.stop()
        player.currentPlaylist.clear()
        'leaving shuffle on during this process slows things down big time.  
        player.settings.setMode("shuffle", False)

        For Each item As Track In trackList
            Dim tempPlayList As IWMPPlaylist = player.mediaCollection.getByAuthor(item.TheArtist.Name)
            Dim count As Integer = tempPlayList.count()

            For index As Integer = 0 To count - 1 Step 1
                Dim currentTitle As String = tempPlayList.Item(index).getItemInfo("Title").Trim
                If (String.Compare(currentTitle, item.Name.Trim, True) = 0) Then
                    player.currentPlaylist.appendItem(tempPlayList.Item(index))
                    Exit For 'Don't need to go any further, and we don't want duplicates either.
                End If
            Next
        Next

        ToggleShuffle()
        FillPlaylistBox()
    End Sub

    Private Sub ToggleShuffle()
        player.settings.setMode("shuffle", ShuffleCheckBox.Checked)
    End Sub

    Private Sub ShuffleCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShuffleCheckBox.CheckedChanged
        ToggleShuffle()
    End Sub

    Private Sub player_CurrentItemChange(ByVal sender As Object, ByVal e As AxWMPLib._WMPOCXEvents_CurrentItemChangeEvent) Handles AxWindowsMediaPlayer1.CurrentItemChange

        ' Display the name of the new media item.
        ArtistTextLabel.Text = player.currentMedia.getItemInfo("Artist")
        AlbumTextLabel.Text = player.currentMedia.getItemInfo("Album")
        TrackTextLabel.Text = player.currentMedia.getItemInfo("Title")
        AlbumYearText.Text = player.currentMedia.getItemInfo("ReleaseDateYear")
        NumberOfItemsText.Text = player.currentPlaylist.count

        Dim currentIndex As Integer = player.currentMedia.getItemInfo("PlaylistIndex")

        If (currentIndex > 0) Then
            PlaylistBox.SelectedIndex = currentIndex
        End If

    End Sub


    Private Sub player_CurrentPlaylistChange(ByVal sender As Object, ByVal e As AxWMPLib._WMPOCXEvents_CurrentPlaylistChangeEvent) Handles AxWindowsMediaPlayer1.CurrentPlaylistChange

        ' Display the name of the new media item.

        NumberOfItemsText.Text = player.currentPlaylist.count
    End Sub

    Private Sub ArtistSimilarityButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArtistSimilarityButton.Click
        Dim name As String = ArtistNameTextBox.Text

        Dim artistList As Artist() = Nothing

        If (Not (name = "")) Then
            artistList = client.GetSimilarArtists(name)
            FilterPlayList(artistList)
        Else
            MsgBox("Please enter an artist first!")
        End If
    End Sub


    Private Sub TrackSimilarityButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackSimilarityButton.Click
        Dim trackName As String = TrackNameTextBox.Text
        Dim artistName As String = TrackArtistTextBox.Text

        Dim trackList As Track() = Nothing

        If (Not (trackName = "") And Not (artistName = "")) Then
            trackList = client.GetSimilarTracks(artistName, trackName)
            FilterPlayList(trackList)
        Else
            MsgBox("Please enter a track and artist first!")
        End If
    End Sub

    Private Sub ArtistTagButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArtistTagButton.Click
        Dim tag As String = ArtistTagTextBox.Text

        Dim artistList As Artist() = Nothing

        If (Not (name = "")) Then
            artistList = client.GetTopArtistsByTag(tag)
            FilterPlayList(artistList)
        Else
            MsgBox("Please enter a tag first!")
        End If
    End Sub

    Private Sub TrackTagButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackTagButton.Click
        Dim tag As String = TrackTagTextBox.Text

        Dim trackList As Track() = Nothing

        If (Not tag = "") Then
            trackList = client.GetTopTracksByTag(tag)
            FilterPlayList(trackList)
        Else
            MsgBox("Please enter a tag first!")
        End If
    End Sub

    Private Sub ArtistByUserButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArtistByUserButton.Click
        Dim user As String = ArtistByUserTextBox.Text

        Dim artistList As Artist() = Nothing

        If (Not (user = "")) Then
            artistList = client.GetTopArtistsByUser(user)
            FilterPlayList(artistList)
        Else
            MsgBox("Please enter a user first!")
        End If
    End Sub

    Private Sub TrackByUserButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackByUserButton.Click
        Dim user As String = TrackByUserTextBox.Text

        Dim trackList As Track() = Nothing

        If (Not user = "") Then
            trackList = client.GetTopTracksByUser(user)
            FilterPlayList(trackList)
        Else
            MsgBox("Please enter a user first!")
        End If
    End Sub
End Class
