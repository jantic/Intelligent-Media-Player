Imports WMPLib
Imports AxWMPLib

Public Class MainInterface
    Private player As AxWindowsMediaPlayer
    Private manager As PlaylistManager
    Private artistInfo As IArtistInfo = Nothing

    Private Sub Initialize() Handles Me.Load
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        InitializePlayer()
        InitializePlaylist()
        InitializePlaylistModifierUI()
    End Sub

    Private Sub InitializePlayer()
        player = AxWindowsMediaPlayer1
    End Sub

    Private Sub InitializePlaylist()
        manager = New PlaylistManager("C:\Users\Jason\Documents\Visual Studio 2010\Projects\IntelligentMediaPlayer\IntelligentMediaPlayer\PlaylistModifierPlugins")

        PlaylistBox.Items.Clear()
        manager.GeneratePlaylist(player)
        FillPlaylistBox()
    End Sub

    Private Sub InitializePlaylistModifierUI()
        AvailablePlaylistModifiersLB.Items.Clear()
        PopulateModifiersListsWithIcons()
        FillAvailableModifierListBox(manager.Liasons)
    End Sub

    Private Sub PopulateModifiersListsWithIcons()
        Try
            Dim imageListSmall As New ImageList()
            Dim LastFMIcon As System.Drawing.Bitmap = GetIconImage("LastFMhq.bmp")
            imageListSmall.Images.Add(GetIconNameForModifierType(PlaylistManager.ModifierType.LastFM), LastFMIcon)
            Dim WMPIcon As System.Drawing.Bitmap = GetIconImage("WMPhq.bmp")
            imageListSmall.Images.Add(GetIconNameForModifierType(PlaylistManager.ModifierType.WMPAttribute), WMPIcon)
            AvailablePlaylistModifiersLB.SmallImageList = imageListSmall
            ActivePlaylistModifiersLB.SmallImageList = imageListSmall
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillAvailableModifierListBox(ByRef liasons As PlaylistManager.PlaylistModifierUILiason())

        For Each liason As PlaylistManager.PlaylistModifierUILiason In liasons
            Dim imageName As String = GetIconNameForModifierType(liason.Type)
            AvailablePlaylistModifiersLB.Items.Add(liason.DisplayName, imageName)
        Next
    End Sub

    Private Function GetIconImage(ByVal imageName As String) As System.Drawing.Bitmap
        'Dim res() As String = GetType(MainInterface).Assembly.GetManifestResourceNames()
        Dim lookUpName As String = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "." + imageName
        Return New System.Drawing.Bitmap(GetType(MainInterface).Assembly.GetManifestResourceStream(lookUpName))
    End Function

    Private Function GetIconNameForModifierType(ByRef modType As PlaylistManager.ModifierType) As String
        Select Case (modType)
            Case PlaylistManager.ModifierType.LastFM
                Return "LastFM"
            Case PlaylistManager.ModifierType.WMPAttribute
                Return "WMPAttribute"
            Case Else
                Return "WMPAttribute"
        End Select
    End Function

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
        UpdateArtistInfo()
        UpdateAlbumInfo()
    End Sub

    Private Sub UpdateAlbumInfo()

    End Sub

    Private Sub UpdateArtistInfo()
        Dim currentArtist As String = player.currentMedia.getItemInfo("Artist")

        Dim needsUpdated As Boolean = False
        If (Not artistInfo Is Nothing) Then
            If (currentArtist.Trim.ToLower <> artistInfo.Name.Trim.ToLower) Then
                needsUpdated = True
            End If
        Else
            needsUpdated = True
        End If

        If (needsUpdated) Then
            'need to clean up memory once in a while with all these images..
            System.GC.Collect()

            artistInfo = New LastFMArtistInfo(currentArtist)
            UpdateBiography()
            UpdateTags()

            Dim topAlbumsThread As Threading.Thread = New Threading.Thread(AddressOf Me.UpdateTopAlbums)
            topAlbumsThread.Start()

            Dim similarArtistThread As Threading.Thread = New Threading.Thread(AddressOf Me.UpdateSimilarArtists)
            similarArtistThread.Start()
        End If
    End Sub



    Private Sub UpdateTopAlbums()
        TopAlbumsLV.Clear()

        Dim albums As IAlbumInfo() = artistInfo.TopAlbums()

        If (Not albums Is Nothing) Then

            Dim albumImages As ImageList = New ImageList()
            albumImages.ImageSize = GetTopAlbumImagesSize()
            albumImages.ColorDepth = ColorDepth.Depth32Bit
            'get album imagelist built first
            For Each album As IAlbumInfo In albums
                Dim albumArt As Image
                If (Not (album.PictureLocation Is Nothing Or album.PictureLocation = "")) Then
                    albumArt = Image.FromFile(album.PictureLocation)
                Else
                    albumArt = GetDefaultAlbumImage()
                End If

                albumImages.Images.Add(album.Name, albumArt)
            Next


            TopAlbumsLV.LargeImageList = albumImages

            For Each album As IAlbumInfo In albums
                TopAlbumsLV.Items.Add(album.Name, album.Name)
            Next
        End If
    End Sub

    Private Function GetSimilarArtistsImagesSize() As Drawing.Size
        Dim imageWidth As UInteger = 150
        Dim imageHeight As UInteger = imageWidth
        Return New System.Drawing.Size(imageWidth, imageHeight)
    End Function

    Private Function GetDefaultArtistImage() As Image
        Return New Bitmap(GetTopAlbumImagesSize().Width, GetTopAlbumImagesSize.Height) ' just a blank bitmap
    End Function


    Private Function GetTopAlbumImagesSize() As Drawing.Size
        Dim imageWidth As UInteger = 90
        Dim imageHeight As UInteger = imageWidth
        Return New System.Drawing.Size(imageWidth, imageHeight)
    End Function

    Private Function GetDefaultAlbumImage() As Image
        Return New Bitmap(GetTopAlbumImagesSize().Width, GetTopAlbumImagesSize.Height) ' just a blank bitmap
    End Function

    Private Sub UpdateTags()
        Dim tags As String() = artistInfo.Tags()



        Dim tagLabels As ArrayList = New ArrayList()

        tagLabels.Add(Tag1)
        tagLabels.Add(Tag2)
        tagLabels.Add(Tag3)
        tagLabels.Add(Tag4)
        tagLabels.Add(Tag5)

        Dim index As UInteger = 0

        'initialize to be invisible
        For Each tagLabel As Windows.Forms.Label In tagLabels
            tagLabel.Visible = False
        Next

        If (Not tags Is Nothing) Then

            For Each tagLabel As Windows.Forms.Label In tagLabels
                If (tags Is Nothing) Then
                    Exit For
                End If

                If (index >= tags.Count) Then
                    Exit For
                End If

                tagLabel.Text = tags.ElementAt(index)
                tagLabel.Visible = True
                index += 1
            Next
        End If
    End Sub

    Private Sub UpdateBiography()
        ArtistPictureBox.ImageLocation = artistInfo.PictureLocation
        FullBioTB.DocumentText = artistInfo.Biography

        If (Not FullBioTB.Document.Body Is Nothing) Then
            Dim zoomLevel As Integer = 10
            FullBioTB.Document.Body.Style = "zoom: " & zoomLevel.ToString & "%"
        End If
    End Sub

    Private Sub UpdateSimilarArtists()

        SimilarArtistsLV.Clear()

        If (Not artistInfo Is Nothing) Then
            Dim similarArtists As IArtistInfo() = artistInfo.SimilarArtists()

            If (Not similarArtists Is Nothing) Then

                Dim index As UInteger = 0


                Dim artistImages As ImageList = New ImageList()
                artistImages.ImageSize = GetSimilarArtistsImagesSize()
                artistImages.ColorDepth = ColorDepth.Depth32Bit

                'get album imagelist built first
                For Each artist As IArtistInfo In similarArtists
                    Dim artistPicture As Image
                    If (Not (artist.PictureLocation Is Nothing Or artist.PictureLocation = "")) Then
                        artistPicture = Image.FromFile(artist.PictureLocation)
                    Else
                        artistPicture = GetDefaultArtistImage()
                    End If


                    artistImages.Images.Add(artist.Name, artistPicture)
                Next


                SimilarArtistsLV.LargeImageList = artistImages

                For Each artist As IArtistInfo In similarArtists
                    SimilarArtistsLV.Items.Add(artist.Name, artist.Name)
                Next
            End If
        End If
    End Sub


    Private Sub player_CurrentPlaylistChange(ByVal sender As Object, ByVal e As AxWMPLib._WMPOCXEvents_CurrentPlaylistChangeEvent) Handles AxWindowsMediaPlayer1.CurrentPlaylistChange

        ' Display the name of the new media item.

        NumberOfItemsText.Text = player.currentPlaylist.count
    End Sub

    Private Sub Initialize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub AddModifierButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddModifierButton.Click

        If (AvailablePlaylistModifiersLB.SelectedItems().Item(0).Index >= 0) Then
            Dim liason As PlaylistManager.PlaylistModifierUILiason = manager.Liasons().ElementAt(AvailablePlaylistModifiersLB.SelectedItems().Item(0).Index)

            Dim myInputTextBoxes As ArrayList = New ArrayList
            myInputTextBoxes.Add(PlaylistModifierInput1)
            myInputTextBoxes.Add(PlaylistModifierInput2)
            myInputTextBoxes.Add(PlaylistModifierInput3)

            Dim index As UInteger = 0

            For Each input As PlaylistManager.PlaylistModifierInput In liason.Inputs
                input.Value = DirectCast(myInputTextBoxes.Item(index), System.Windows.Forms.MaskedTextBox).Text
                index += 1
            Next

            liason.ModifierAction = GetSelectedAction()

            manager.AddWorkingModifier(liason)

            For Each inputTextBox As Windows.Forms.MaskedTextBox In myInputTextBoxes
                inputTextBox.Clear()
            Next

            UpdateActiveModifiersLB()
        End If
    End Sub

    Private Function GetSelectedAction() As PlaylistManager.IModifierAction
        If (FilterRB.Checked) Then
            Return New PlaylistManager.ModifierAction_Filter
        ElseIf (AddRB.Checked) Then
            Return New PlaylistManager.ModifierAction_Add
        Else
            Return New PlaylistManager.ModifierAction_Subtract
        End If
    End Function

    Private Sub SetSelectedAction(ByVal theAction As PlaylistManager.IModifierAction)
        If (theAction.Name = "Add") Then
            AddRB.Checked = True
        ElseIf (theAction.Name = "Filter") Then
            FilterRB.Checked = True
        Else
            RemoveRb.Checked = True
        End If
    End Sub

    Private Sub RemoveModifierButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveModifierButton.Click
        If (ActivePlaylistModifiersLB.SelectedItems().Count > 0) Then
            Dim selectedIndex As Integer = ActivePlaylistModifiersLB.SelectedItems().Item(0).Index
            If ((selectedIndex <= manager.GetWorkingModifiers.Length - 1) And selectedIndex > -1) Then
                manager.RemoveWorkingModifier(selectedIndex)
                UpdateActiveModifiersLB()
            End If
        End If
    End Sub


    Private Sub AvailablePlaylistModifiersLB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AvailablePlaylistModifiersLB.SelectedIndexChanged
        If (AvailablePlaylistModifiersLB.SelectedItems().Count > 0) Then
            UpdateModiferInputsUI(manager.Liasons().ElementAt(AvailablePlaylistModifiersLB.SelectedItems().Item(0).Index))
        End If
    End Sub

    Private Sub UpdateModiferInputsUI(ByRef liason As PlaylistManager.PlaylistModifierUILiason)
        Dim myInputLabels As ArrayList = New ArrayList
        myInputLabels.Add(Input1Label)
        myInputLabels.Add(Input2Label)
        myInputLabels.Add(Input3Label)

        For Each inputLabel As System.Windows.Forms.Label In myInputLabels
            inputLabel.ResetText()
            inputLabel.Visible = False
        Next

        Dim myInputTextBoxes As ArrayList = New ArrayList
        myInputTextBoxes.Add(PlaylistModifierInput1)
        myInputTextBoxes.Add(PlaylistModifierInput2)
        myInputTextBoxes.Add(PlaylistModifierInput3)


        For Each inputTextBox As System.Windows.Forms.MaskedTextBox In myInputTextBoxes
            inputTextBox.Clear()
            inputTextBox.Visible = False
        Next

        Dim index As UInteger = 0

        For Each input As PlaylistManager.PlaylistModifierInput In liason.Inputs
            DirectCast(myInputLabels.Item(index), System.Windows.Forms.Label).Visible = True
            DirectCast(myInputLabels.Item(index), System.Windows.Forms.Label).Text = input.DisplayName
            DirectCast(myInputTextBoxes.Item(index), System.Windows.Forms.MaskedTextBox).Visible = True
            index += 1
        Next

        SetSelectedAction(liason.ModifierAction)

    End Sub

    Private Sub UpdateActiveModifiersLB()
        ActivePlaylistModifiersLB.Items.Clear()

        Dim workingModifiers As PlaylistManager.PlaylistModifierUILiason() = manager.GetWorkingModifiers()

        If (Not (workingModifiers Is Nothing)) Then
            For Each liason As PlaylistManager.PlaylistModifierUILiason In workingModifiers

                Dim displayText As String = "[" + liason.ModifierAction.Name + "] " + liason.DisplayName + "("

                For Each input As PlaylistManager.PlaylistModifierInput In liason.Inputs
                    displayText = displayText + input.DisplayName + " - " + input.Value + ";"
                Next

                displayText = displayText.TrimEnd(";")
                displayText = displayText + ")"
                Dim imageName As String = GetIconNameForModifierType(liason.Type)
                ActivePlaylistModifiersLB.Items.Add(displayText, imageName)
            Next
        End If

    End Sub


    Private Sub GeneratePlaylistButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GeneratePlaylistButton.Click
        player.Ctlcontrols.stop()
        'leaving shuffle on during this process slows things down big time.  
        player.settings.setMode("shuffle", False)

        manager.GeneratePlaylist(player)

        ToggleShuffle()
        FillPlaylistBox()
    End Sub


    Private Function GetTorrentzURLForArtistSearch(ByVal artist As String) As String
        Dim baseURL As String = "http://www.torrentz.com/search?q="
        Dim searchString As String = artist.Replace(" ", "+")
        Return baseURL + searchString
    End Function

    Private Sub SimilarArtistsLV_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimilarArtistsLV.DoubleClick

        If (Not SimilarArtistsLV.SelectedIndices Is Nothing) Then

            If (SimilarArtistsLV.SelectedIndices.Count > 0) Then

                Dim selectedArtist As String = SimilarArtistsLV.Items.Item(SimilarArtistsLV.SelectedIndices(0)).Text
                System.Diagnostics.Process.Start(GetTorrentzURLForArtistSearch(selectedArtist))
            End If
        End If
    End Sub
End Class
