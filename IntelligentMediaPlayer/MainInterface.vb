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
            Dim trackName As String = media.getItemInfo("Author") + " - " + media.getItemInfo("Album") + ": " + media.name
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

    Private Sub GeneratePlayList(ByRef artistList As Artist())
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

        FilterCurrentPlayList()
        ToggleShuffle()
        FillPlaylistBox()
    End Sub


    Private Sub GeneratePlayList(ByRef trackList As Track())
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

        FilterCurrentPlayList()
        ToggleShuffle()
        FillPlaylistBox()
    End Sub

    Private Sub GeneratePlayList(ByRef albumList As Album())
        player.Ctlcontrols.stop()
        player.currentPlaylist.clear()
        'leaving shuffle on during this process slows things down big time.  
        player.settings.setMode("shuffle", False)

        For Each item As Album In albumList
            Dim tempPlayList As IWMPPlaylist = player.mediaCollection.getByAlbum(item.Name)
            Dim count As Integer = tempPlayList.count()

            For index As Integer = 0 To count - 1 Step 1
                Dim currentTitle As String = tempPlayList.Item(index).getItemInfo("Album").Trim
                If (String.Compare(currentTitle, item.Name.Trim, True) = 0) Then
                    player.currentPlaylist.appendItem(tempPlayList.Item(index))
                End If
            Next
        Next

        FilterCurrentPlayList()
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
            GeneratePlayList(artistList)
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
            GeneratePlayList(trackList)
        Else
            MsgBox("Please enter a track and artist first!")
        End If
    End Sub

    Private Sub ArtistTagButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArtistTagButton.Click
        Dim tag As String = ArtistTagTextBox.Text

        Dim artistList As Artist() = Nothing

        If (Not (name = "")) Then
            artistList = client.GetTopArtistsByTag(tag)
            GeneratePlayList(artistList)
        Else
            MsgBox("Please enter a tag first!")
        End If
    End Sub

    Private Sub TrackTagButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackTagButton.Click
        Dim tag As String = TrackTagTextBox.Text

        Dim trackList As Track() = Nothing

        If (Not tag = "") Then
            trackList = client.GetTopTracksByTag(tag)
            GeneratePlayList(trackList)
        Else
            MsgBox("Please enter a tag first!")
        End If
    End Sub

    Private Sub ArtistByUserButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArtistByUserButton.Click
        Dim user As String = ArtistByUserTextBox.Text

        Dim artistList As Artist() = Nothing

        If (Not (user = "")) Then
            artistList = client.GetTopArtistsByUser(user)
            GeneratePlayList(artistList)
        Else
            MsgBox("Please enter a user first!")
        End If
    End Sub

    Private Sub TrackByUserButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackByUserButton.Click
        Dim user As String = TrackByUserTextBox.Text

        Dim trackList As Track() = Nothing

        If (Not user = "") Then
            trackList = client.GetTopTracksByUser(user)
            GeneratePlayList(trackList)
        Else
            MsgBox("Please enter a user first!")
        End If
    End Sub

    Private Sub AlbumTagButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlbumTagButton.Click
        Dim tag As String = AlbumTagTextBox.Text

        Dim albumList As Album() = Nothing

        If (Not tag = "") Then
            albumList = client.GetTopAlbumsByTag(tag)
            GeneratePlayList(albumList)
        Else
            MsgBox("Please enter a tag first!")
        End If
    End Sub

    Private Sub Initialize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub AddFilteredOutArtistButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddFilteredOutArtistButton.Click
        Dim artistToAddToFilter As String = ArtistFilterOutTextBox.Text.Trim.ToLower

        If (Not (artistToAddToFilter = "" And FilteredOutArtistsLB.FindString(artistToAddToFilter) >= 0)) Then
            FilteredOutArtistsLB.Items.Add(artistToAddToFilter)
            FilterCurrentPlayList()
            PlaylistBox.Items.Clear()
            FillPlaylistBox()
            ArtistFilterOutTextBox.Clear()
        End If
    End Sub

    Private Sub RemoveFilteredOutArtistButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveFilteredOutArtistButton.Click
        Dim artistToRemoveFromFilter As String = ArtistFilterOutTextBox.Text.Trim.ToLower

        Dim removeIndex As Integer = FilteredOutArtistsLB.FindString(artistToRemoveFromFilter)

        If (Not (artistToRemoveFromFilter = "") And removeIndex >= 0) Then
            FilteredOutArtistsLB.Items.RemoveAt(removeIndex)
            FilterCurrentPlayList()
            PlaylistBox.Items.Clear()
            FillPlaylistBox()
            ArtistFilterOutTextBox.Clear()
        End If
    End Sub


    Private Sub FilterCurrentPlayList()
        'player.Ctlcontrols.stop()
        'leaving shuffle on during this process slows things down big time.  
        player.settings.setMode("shuffle", False)

        Dim count As Integer = player.currentPlaylist.count

        Dim removeList As ArrayList = New ArrayList

        For Each filteredArtist As String In FilteredOutArtistsLB.Items
            For index As Integer = 0 To count - 1 Step 1
                Dim currentArtist As String = player.currentPlaylist.Item(index).getItemInfo("Artist").Trim.ToLower
                If (String.Compare(currentArtist, filteredArtist.Trim.ToLower, True) = 0) Then
                    removeList.Add(player.currentPlaylist.Item(index))
                End If
            Next
        Next


        For Each toRemove As WMPLib.IWMPMedia In removeList
            player.currentPlaylist.removeItem(toRemove)
        Next

        ToggleShuffle()
    End Sub

 
    Private Sub FilteredOutArtistsLB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilteredOutArtistsLB.SelectedIndexChanged
        ArtistFilterOutTextBox.Text = FilteredOutArtistsLB.Text
    End Sub
End Class
