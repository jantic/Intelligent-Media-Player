<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainInterface
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainInterface))
        Me.AxWindowsMediaPlayer1 = New AxWMPLib.AxWindowsMediaPlayer()
        Me.PlaylistBox = New System.Windows.Forms.ListBox()
        Me.FilteredPlaylistLabel = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ArtistSimilarityButton = New System.Windows.Forms.Button()
        Me.ArtistNameLabel = New System.Windows.Forms.Label()
        Me.ArtistNameTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TrackArtistLabel = New System.Windows.Forms.Label()
        Me.TrackArtistTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.TrackSimilarityButton = New System.Windows.Forms.Button()
        Me.TrackNameTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.TrackNameLabel = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.ArtistTagButton = New System.Windows.Forms.Button()
        Me.ArtistTagTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.ArtistTagLabel = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TrackTagButton = New System.Windows.Forms.Button()
        Me.TrackTagTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.TrackTagLabel = New System.Windows.Forms.Label()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.AlbumTagButton = New System.Windows.Forms.Button()
        Me.AlbumTagTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.AlbumTagLabel = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.ArtistByUserButton = New System.Windows.Forms.Button()
        Me.ArtistByUserLabel = New System.Windows.Forms.Label()
        Me.ArtistByUserTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.TrackByUserButton = New System.Windows.Forms.Button()
        Me.TrackByUserLabel = New System.Windows.Forms.Label()
        Me.TrackByUserTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.NumberOfItemsLabel = New System.Windows.Forms.Label()
        Me.NumberOfItemsText = New System.Windows.Forms.Label()
        Me.PlaylistGeneratorsLabel = New System.Windows.Forms.Label()
        Me.NowPlayingGB = New System.Windows.Forms.GroupBox()
        Me.AlbumYearText = New System.Windows.Forms.Label()
        Me.AlbumYearLabel = New System.Windows.Forms.Label()
        Me.TrackTextLabel = New System.Windows.Forms.Label()
        Me.AlbumTextLabel = New System.Windows.Forms.Label()
        Me.ArtistTextLabel = New System.Windows.Forms.Label()
        Me.TagTrackLabel = New System.Windows.Forms.Label()
        Me.TagAlbumLabel = New System.Windows.Forms.Label()
        Me.TagArtistLabel = New System.Windows.Forms.Label()
        Me.ShuffleCheckBox = New System.Windows.Forms.CheckBox()
        Me.ArtistFilterGB = New System.Windows.Forms.GroupBox()
        Me.FilteredOutArtistsLB = New System.Windows.Forms.ListBox()
        Me.RemoveFilteredOutArtistButton = New System.Windows.Forms.Button()
        Me.AddFilteredOutArtistButton = New System.Windows.Forms.Button()
        Me.ArtistFilterOutTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.NowPlayingGB.SuspendLayout()
        Me.ArtistFilterGB.SuspendLayout()
        Me.SuspendLayout()
        '
        'AxWindowsMediaPlayer1
        '
        Me.AxWindowsMediaPlayer1.Enabled = True
        Me.AxWindowsMediaPlayer1.Location = New System.Drawing.Point(579, 25)
        Me.AxWindowsMediaPlayer1.Name = "AxWindowsMediaPlayer1"
        Me.AxWindowsMediaPlayer1.OcxState = CType(resources.GetObject("AxWindowsMediaPlayer1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxWindowsMediaPlayer1.Size = New System.Drawing.Size(278, 212)
        Me.AxWindowsMediaPlayer1.TabIndex = 0
        '
        'PlaylistBox
        '
        Me.PlaylistBox.FormattingEnabled = True
        Me.PlaylistBox.Location = New System.Drawing.Point(12, 25)
        Me.PlaylistBox.Name = "PlaylistBox"
        Me.PlaylistBox.Size = New System.Drawing.Size(549, 212)
        Me.PlaylistBox.TabIndex = 1
        '
        'FilteredPlaylistLabel
        '
        Me.FilteredPlaylistLabel.AutoSize = True
        Me.FilteredPlaylistLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FilteredPlaylistLabel.Location = New System.Drawing.Point(9, 9)
        Me.FilteredPlaylistLabel.Name = "FilteredPlaylistLabel"
        Me.FilteredPlaylistLabel.Size = New System.Drawing.Size(76, 13)
        Me.FilteredPlaylistLabel.TabIndex = 4
        Me.FilteredPlaylistLabel.Text = "Filtered Playlist"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Location = New System.Drawing.Point(11, 466)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(553, 200)
        Me.TabControl1.TabIndex = 14
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ArtistSimilarityButton)
        Me.TabPage1.Controls.Add(Me.ArtistNameLabel)
        Me.TabPage1.Controls.Add(Me.ArtistNameTextBox)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(545, 174)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Artist Similarity"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ArtistSimilarityButton
        '
        Me.ArtistSimilarityButton.Location = New System.Drawing.Point(21, 141)
        Me.ArtistSimilarityButton.Name = "ArtistSimilarityButton"
        Me.ArtistSimilarityButton.Size = New System.Drawing.Size(265, 27)
        Me.ArtistSimilarityButton.TabIndex = 8
        Me.ArtistSimilarityButton.Text = "Regenerate Playlist by Artist Similarity"
        Me.ArtistSimilarityButton.UseVisualStyleBackColor = True
        '
        'ArtistNameLabel
        '
        Me.ArtistNameLabel.AutoSize = True
        Me.ArtistNameLabel.Location = New System.Drawing.Point(18, 17)
        Me.ArtistNameLabel.Name = "ArtistNameLabel"
        Me.ArtistNameLabel.Size = New System.Drawing.Size(30, 13)
        Me.ArtistNameLabel.TabIndex = 7
        Me.ArtistNameLabel.Text = "Artist"
        '
        'ArtistNameTextBox
        '
        Me.ArtistNameTextBox.Location = New System.Drawing.Point(93, 14)
        Me.ArtistNameTextBox.Name = "ArtistNameTextBox"
        Me.ArtistNameTextBox.Size = New System.Drawing.Size(193, 20)
        Me.ArtistNameTextBox.TabIndex = 6
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TrackArtistLabel)
        Me.TabPage2.Controls.Add(Me.TrackArtistTextBox)
        Me.TabPage2.Controls.Add(Me.TrackSimilarityButton)
        Me.TabPage2.Controls.Add(Me.TrackNameTextBox)
        Me.TabPage2.Controls.Add(Me.TrackNameLabel)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(545, 174)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Track Similarity"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TrackArtistLabel
        '
        Me.TrackArtistLabel.AutoSize = True
        Me.TrackArtistLabel.Location = New System.Drawing.Point(18, 17)
        Me.TrackArtistLabel.Name = "TrackArtistLabel"
        Me.TrackArtistLabel.Size = New System.Drawing.Size(30, 13)
        Me.TrackArtistLabel.TabIndex = 7
        Me.TrackArtistLabel.Text = "Artist"
        '
        'TrackArtistTextBox
        '
        Me.TrackArtistTextBox.Location = New System.Drawing.Point(93, 14)
        Me.TrackArtistTextBox.Name = "TrackArtistTextBox"
        Me.TrackArtistTextBox.Size = New System.Drawing.Size(200, 20)
        Me.TrackArtistTextBox.TabIndex = 6
        '
        'TrackSimilarityButton
        '
        Me.TrackSimilarityButton.Location = New System.Drawing.Point(21, 141)
        Me.TrackSimilarityButton.Name = "TrackSimilarityButton"
        Me.TrackSimilarityButton.Size = New System.Drawing.Size(265, 27)
        Me.TrackSimilarityButton.TabIndex = 5
        Me.TrackSimilarityButton.Text = "Regenerate Playlist by Track Similarity"
        Me.TrackSimilarityButton.UseVisualStyleBackColor = True
        '
        'TrackNameTextBox
        '
        Me.TrackNameTextBox.Location = New System.Drawing.Point(93, 56)
        Me.TrackNameTextBox.Name = "TrackNameTextBox"
        Me.TrackNameTextBox.Size = New System.Drawing.Size(200, 20)
        Me.TrackNameTextBox.TabIndex = 4
        '
        'TrackNameLabel
        '
        Me.TrackNameLabel.AutoSize = True
        Me.TrackNameLabel.Location = New System.Drawing.Point(18, 59)
        Me.TrackNameLabel.Name = "TrackNameLabel"
        Me.TrackNameLabel.Size = New System.Drawing.Size(66, 13)
        Me.TrackNameLabel.TabIndex = 3
        Me.TrackNameLabel.Text = "Track Name"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.ArtistTagButton)
        Me.TabPage3.Controls.Add(Me.ArtistTagTextBox)
        Me.TabPage3.Controls.Add(Me.ArtistTagLabel)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(545, 174)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Artist Tag"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'ArtistTagButton
        '
        Me.ArtistTagButton.Location = New System.Drawing.Point(21, 141)
        Me.ArtistTagButton.Name = "ArtistTagButton"
        Me.ArtistTagButton.Size = New System.Drawing.Size(265, 27)
        Me.ArtistTagButton.TabIndex = 2
        Me.ArtistTagButton.Text = "Regenerate Playlist by Artist Tag"
        Me.ArtistTagButton.UseVisualStyleBackColor = True
        '
        'ArtistTagTextBox
        '
        Me.ArtistTagTextBox.Location = New System.Drawing.Point(93, 14)
        Me.ArtistTagTextBox.Name = "ArtistTagTextBox"
        Me.ArtistTagTextBox.Size = New System.Drawing.Size(200, 20)
        Me.ArtistTagTextBox.TabIndex = 1
        '
        'ArtistTagLabel
        '
        Me.ArtistTagLabel.AutoSize = True
        Me.ArtistTagLabel.Location = New System.Drawing.Point(18, 17)
        Me.ArtistTagLabel.Name = "ArtistTagLabel"
        Me.ArtistTagLabel.Size = New System.Drawing.Size(26, 13)
        Me.ArtistTagLabel.TabIndex = 0
        Me.ArtistTagLabel.Text = "Tag"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.TrackTagButton)
        Me.TabPage4.Controls.Add(Me.TrackTagTextBox)
        Me.TabPage4.Controls.Add(Me.TrackTagLabel)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(545, 174)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Track Tag"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'TrackTagButton
        '
        Me.TrackTagButton.Location = New System.Drawing.Point(21, 141)
        Me.TrackTagButton.Name = "TrackTagButton"
        Me.TrackTagButton.Size = New System.Drawing.Size(265, 27)
        Me.TrackTagButton.TabIndex = 4
        Me.TrackTagButton.Text = "Regenerate Playlist by Track Tag"
        Me.TrackTagButton.UseVisualStyleBackColor = True
        '
        'TrackTagTextBox
        '
        Me.TrackTagTextBox.Location = New System.Drawing.Point(93, 14)
        Me.TrackTagTextBox.Name = "TrackTagTextBox"
        Me.TrackTagTextBox.Size = New System.Drawing.Size(200, 20)
        Me.TrackTagTextBox.TabIndex = 3
        '
        'TrackTagLabel
        '
        Me.TrackTagLabel.AutoSize = True
        Me.TrackTagLabel.Location = New System.Drawing.Point(18, 17)
        Me.TrackTagLabel.Name = "TrackTagLabel"
        Me.TrackTagLabel.Size = New System.Drawing.Size(26, 13)
        Me.TrackTagLabel.TabIndex = 2
        Me.TrackTagLabel.Text = "Tag"
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.AlbumTagButton)
        Me.TabPage7.Controls.Add(Me.AlbumTagTextBox)
        Me.TabPage7.Controls.Add(Me.AlbumTagLabel)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage7.Size = New System.Drawing.Size(545, 174)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "Album Tag"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'AlbumTagButton
        '
        Me.AlbumTagButton.Location = New System.Drawing.Point(21, 141)
        Me.AlbumTagButton.Name = "AlbumTagButton"
        Me.AlbumTagButton.Size = New System.Drawing.Size(265, 27)
        Me.AlbumTagButton.TabIndex = 7
        Me.AlbumTagButton.Text = "Regenerate Playlist by Album Tag"
        Me.AlbumTagButton.UseVisualStyleBackColor = True
        '
        'AlbumTagTextBox
        '
        Me.AlbumTagTextBox.Location = New System.Drawing.Point(93, 14)
        Me.AlbumTagTextBox.Name = "AlbumTagTextBox"
        Me.AlbumTagTextBox.Size = New System.Drawing.Size(200, 20)
        Me.AlbumTagTextBox.TabIndex = 6
        '
        'AlbumTagLabel
        '
        Me.AlbumTagLabel.AutoSize = True
        Me.AlbumTagLabel.Location = New System.Drawing.Point(18, 17)
        Me.AlbumTagLabel.Name = "AlbumTagLabel"
        Me.AlbumTagLabel.Size = New System.Drawing.Size(26, 13)
        Me.AlbumTagLabel.TabIndex = 5
        Me.AlbumTagLabel.Text = "Tag"
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.ArtistByUserButton)
        Me.TabPage5.Controls.Add(Me.ArtistByUserLabel)
        Me.TabPage5.Controls.Add(Me.ArtistByUserTextBox)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(545, 174)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "User's Top Artists"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'ArtistByUserButton
        '
        Me.ArtistByUserButton.Location = New System.Drawing.Point(21, 141)
        Me.ArtistByUserButton.Name = "ArtistByUserButton"
        Me.ArtistByUserButton.Size = New System.Drawing.Size(265, 27)
        Me.ArtistByUserButton.TabIndex = 11
        Me.ArtistByUserButton.Text = "Regenerate Playlist by User's Top Artists"
        Me.ArtistByUserButton.UseVisualStyleBackColor = True
        '
        'ArtistByUserLabel
        '
        Me.ArtistByUserLabel.AutoSize = True
        Me.ArtistByUserLabel.Location = New System.Drawing.Point(18, 17)
        Me.ArtistByUserLabel.Name = "ArtistByUserLabel"
        Me.ArtistByUserLabel.Size = New System.Drawing.Size(29, 13)
        Me.ArtistByUserLabel.TabIndex = 10
        Me.ArtistByUserLabel.Text = "User"
        '
        'ArtistByUserTextBox
        '
        Me.ArtistByUserTextBox.Location = New System.Drawing.Point(93, 14)
        Me.ArtistByUserTextBox.Name = "ArtistByUserTextBox"
        Me.ArtistByUserTextBox.Size = New System.Drawing.Size(193, 20)
        Me.ArtistByUserTextBox.TabIndex = 9
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.TrackByUserButton)
        Me.TabPage6.Controls.Add(Me.TrackByUserLabel)
        Me.TabPage6.Controls.Add(Me.TrackByUserTextBox)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(545, 174)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "User's Top Tracks"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'TrackByUserButton
        '
        Me.TrackByUserButton.Location = New System.Drawing.Point(21, 141)
        Me.TrackByUserButton.Name = "TrackByUserButton"
        Me.TrackByUserButton.Size = New System.Drawing.Size(265, 27)
        Me.TrackByUserButton.TabIndex = 14
        Me.TrackByUserButton.Text = "Regenerate Playlist by User's Top Tracks"
        Me.TrackByUserButton.UseVisualStyleBackColor = True
        '
        'TrackByUserLabel
        '
        Me.TrackByUserLabel.AutoSize = True
        Me.TrackByUserLabel.Location = New System.Drawing.Point(18, 17)
        Me.TrackByUserLabel.Name = "TrackByUserLabel"
        Me.TrackByUserLabel.Size = New System.Drawing.Size(29, 13)
        Me.TrackByUserLabel.TabIndex = 13
        Me.TrackByUserLabel.Text = "User"
        '
        'TrackByUserTextBox
        '
        Me.TrackByUserTextBox.Location = New System.Drawing.Point(93, 14)
        Me.TrackByUserTextBox.Name = "TrackByUserTextBox"
        Me.TrackByUserTextBox.Size = New System.Drawing.Size(193, 20)
        Me.TrackByUserTextBox.TabIndex = 12
        '
        'NumberOfItemsLabel
        '
        Me.NumberOfItemsLabel.AutoSize = True
        Me.NumberOfItemsLabel.Location = New System.Drawing.Point(9, 240)
        Me.NumberOfItemsLabel.Name = "NumberOfItemsLabel"
        Me.NumberOfItemsLabel.Size = New System.Drawing.Size(87, 13)
        Me.NumberOfItemsLabel.TabIndex = 17
        Me.NumberOfItemsLabel.Text = "Number of Items:"
        '
        'NumberOfItemsText
        '
        Me.NumberOfItemsText.AutoSize = True
        Me.NumberOfItemsText.Location = New System.Drawing.Point(102, 240)
        Me.NumberOfItemsText.Name = "NumberOfItemsText"
        Me.NumberOfItemsText.Size = New System.Drawing.Size(0, 13)
        Me.NumberOfItemsText.TabIndex = 18
        '
        'PlaylistGeneratorsLabel
        '
        Me.PlaylistGeneratorsLabel.AutoSize = True
        Me.PlaylistGeneratorsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PlaylistGeneratorsLabel.Location = New System.Drawing.Point(8, 449)
        Me.PlaylistGeneratorsLabel.Name = "PlaylistGeneratorsLabel"
        Me.PlaylistGeneratorsLabel.Size = New System.Drawing.Size(94, 13)
        Me.PlaylistGeneratorsLabel.TabIndex = 19
        Me.PlaylistGeneratorsLabel.Text = "Playlist Generators"
        '
        'NowPlayingGB
        '
        Me.NowPlayingGB.Controls.Add(Me.AlbumYearText)
        Me.NowPlayingGB.Controls.Add(Me.AlbumYearLabel)
        Me.NowPlayingGB.Controls.Add(Me.TrackTextLabel)
        Me.NowPlayingGB.Controls.Add(Me.AlbumTextLabel)
        Me.NowPlayingGB.Controls.Add(Me.ArtistTextLabel)
        Me.NowPlayingGB.Controls.Add(Me.TagTrackLabel)
        Me.NowPlayingGB.Controls.Add(Me.TagAlbumLabel)
        Me.NowPlayingGB.Controls.Add(Me.TagArtistLabel)
        Me.NowPlayingGB.Controls.Add(Me.ShuffleCheckBox)
        Me.NowPlayingGB.Location = New System.Drawing.Point(580, 249)
        Me.NowPlayingGB.Name = "NowPlayingGB"
        Me.NowPlayingGB.Size = New System.Drawing.Size(276, 130)
        Me.NowPlayingGB.TabIndex = 20
        Me.NowPlayingGB.TabStop = False
        Me.NowPlayingGB.Text = "Now Playing"
        '
        'AlbumYearText
        '
        Me.AlbumYearText.AutoSize = True
        Me.AlbumYearText.Location = New System.Drawing.Point(50, 84)
        Me.AlbumYearText.Name = "AlbumYearText"
        Me.AlbumYearText.Size = New System.Drawing.Size(0, 13)
        Me.AlbumYearText.TabIndex = 25
        '
        'AlbumYearLabel
        '
        Me.AlbumYearLabel.AutoSize = True
        Me.AlbumYearLabel.Location = New System.Drawing.Point(7, 84)
        Me.AlbumYearLabel.Name = "AlbumYearLabel"
        Me.AlbumYearLabel.Size = New System.Drawing.Size(32, 13)
        Me.AlbumYearLabel.TabIndex = 24
        Me.AlbumYearLabel.Text = "Year:"
        '
        'TrackTextLabel
        '
        Me.TrackTextLabel.AutoSize = True
        Me.TrackTextLabel.Location = New System.Drawing.Point(50, 66)
        Me.TrackTextLabel.Name = "TrackTextLabel"
        Me.TrackTextLabel.Size = New System.Drawing.Size(0, 13)
        Me.TrackTextLabel.TabIndex = 23
        '
        'AlbumTextLabel
        '
        Me.AlbumTextLabel.AutoSize = True
        Me.AlbumTextLabel.Location = New System.Drawing.Point(50, 47)
        Me.AlbumTextLabel.Name = "AlbumTextLabel"
        Me.AlbumTextLabel.Size = New System.Drawing.Size(0, 13)
        Me.AlbumTextLabel.TabIndex = 22
        '
        'ArtistTextLabel
        '
        Me.ArtistTextLabel.AutoSize = True
        Me.ArtistTextLabel.Location = New System.Drawing.Point(50, 27)
        Me.ArtistTextLabel.Name = "ArtistTextLabel"
        Me.ArtistTextLabel.Size = New System.Drawing.Size(0, 13)
        Me.ArtistTextLabel.TabIndex = 21
        '
        'TagTrackLabel
        '
        Me.TagTrackLabel.AutoSize = True
        Me.TagTrackLabel.Location = New System.Drawing.Point(6, 66)
        Me.TagTrackLabel.Name = "TagTrackLabel"
        Me.TagTrackLabel.Size = New System.Drawing.Size(38, 13)
        Me.TagTrackLabel.TabIndex = 20
        Me.TagTrackLabel.Text = "Track:"
        '
        'TagAlbumLabel
        '
        Me.TagAlbumLabel.AutoSize = True
        Me.TagAlbumLabel.Location = New System.Drawing.Point(6, 47)
        Me.TagAlbumLabel.Name = "TagAlbumLabel"
        Me.TagAlbumLabel.Size = New System.Drawing.Size(39, 13)
        Me.TagAlbumLabel.TabIndex = 19
        Me.TagAlbumLabel.Text = "Album:"
        '
        'TagArtistLabel
        '
        Me.TagArtistLabel.AutoSize = True
        Me.TagArtistLabel.Location = New System.Drawing.Point(6, 27)
        Me.TagArtistLabel.Name = "TagArtistLabel"
        Me.TagArtistLabel.Size = New System.Drawing.Size(36, 13)
        Me.TagArtistLabel.TabIndex = 18
        Me.TagArtistLabel.Text = "Artist: "
        '
        'ShuffleCheckBox
        '
        Me.ShuffleCheckBox.AutoSize = True
        Me.ShuffleCheckBox.Location = New System.Drawing.Point(9, 108)
        Me.ShuffleCheckBox.Name = "ShuffleCheckBox"
        Me.ShuffleCheckBox.Size = New System.Drawing.Size(59, 17)
        Me.ShuffleCheckBox.TabIndex = 17
        Me.ShuffleCheckBox.Text = "Shuffle"
        Me.ShuffleCheckBox.UseVisualStyleBackColor = True
        '
        'ArtistFilterGB
        '
        Me.ArtistFilterGB.Controls.Add(Me.FilteredOutArtistsLB)
        Me.ArtistFilterGB.Controls.Add(Me.RemoveFilteredOutArtistButton)
        Me.ArtistFilterGB.Controls.Add(Me.AddFilteredOutArtistButton)
        Me.ArtistFilterGB.Controls.Add(Me.ArtistFilterOutTextBox)
        Me.ArtistFilterGB.Location = New System.Drawing.Point(12, 268)
        Me.ArtistFilterGB.Name = "ArtistFilterGB"
        Me.ArtistFilterGB.Size = New System.Drawing.Size(262, 166)
        Me.ArtistFilterGB.TabIndex = 21
        Me.ArtistFilterGB.TabStop = False
        Me.ArtistFilterGB.Text = "Artists to Filter Out"
        '
        'FilteredOutArtistsLB
        '
        Me.FilteredOutArtistsLB.FormattingEnabled = True
        Me.FilteredOutArtistsLB.Location = New System.Drawing.Point(10, 55)
        Me.FilteredOutArtistsLB.Name = "FilteredOutArtistsLB"
        Me.FilteredOutArtistsLB.Size = New System.Drawing.Size(130, 95)
        Me.FilteredOutArtistsLB.TabIndex = 4
        '
        'RemoveFilteredOutArtistButton
        '
        Me.RemoveFilteredOutArtistButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RemoveFilteredOutArtistButton.Location = New System.Drawing.Point(193, 19)
        Me.RemoveFilteredOutArtistButton.Name = "RemoveFilteredOutArtistButton"
        Me.RemoveFilteredOutArtistButton.Size = New System.Drawing.Size(39, 35)
        Me.RemoveFilteredOutArtistButton.TabIndex = 3
        Me.RemoveFilteredOutArtistButton.Text = "-"
        Me.RemoveFilteredOutArtistButton.UseVisualStyleBackColor = True
        '
        'AddFilteredOutArtistButton
        '
        Me.AddFilteredOutArtistButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddFilteredOutArtistButton.Location = New System.Drawing.Point(147, 19)
        Me.AddFilteredOutArtistButton.Name = "AddFilteredOutArtistButton"
        Me.AddFilteredOutArtistButton.Size = New System.Drawing.Size(39, 35)
        Me.AddFilteredOutArtistButton.TabIndex = 2
        Me.AddFilteredOutArtistButton.Text = "+"
        Me.AddFilteredOutArtistButton.UseVisualStyleBackColor = True
        '
        'ArtistFilterOutTextBox
        '
        Me.ArtistFilterOutTextBox.Location = New System.Drawing.Point(10, 28)
        Me.ArtistFilterOutTextBox.Name = "ArtistFilterOutTextBox"
        Me.ArtistFilterOutTextBox.Size = New System.Drawing.Size(130, 20)
        Me.ArtistFilterOutTextBox.TabIndex = 1
        '
        'MainInterface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 678)
        Me.Controls.Add(Me.ArtistFilterGB)
        Me.Controls.Add(Me.NowPlayingGB)
        Me.Controls.Add(Me.PlaylistGeneratorsLabel)
        Me.Controls.Add(Me.NumberOfItemsText)
        Me.Controls.Add(Me.NumberOfItemsLabel)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.FilteredPlaylistLabel)
        Me.Controls.Add(Me.PlaylistBox)
        Me.Controls.Add(Me.AxWindowsMediaPlayer1)
        Me.MinimumSize = New System.Drawing.Size(885, 420)
        Me.Name = "MainInterface"
        Me.Text = "Intelligent Media Player"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.TabPage7.ResumeLayout(False)
        Me.TabPage7.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage6.PerformLayout()
        Me.NowPlayingGB.ResumeLayout(False)
        Me.NowPlayingGB.PerformLayout()
        Me.ArtistFilterGB.ResumeLayout(False)
        Me.ArtistFilterGB.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AxWindowsMediaPlayer1 As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents PlaylistBox As System.Windows.Forms.ListBox
    Friend WithEvents FilteredPlaylistLabel As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents ArtistSimilarityButton As System.Windows.Forms.Button
    Friend WithEvents ArtistNameLabel As System.Windows.Forms.Label
    Friend WithEvents ArtistNameTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TrackArtistLabel As System.Windows.Forms.Label
    Friend WithEvents TrackArtistTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TrackSimilarityButton As System.Windows.Forms.Button
    Friend WithEvents TrackNameTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TrackNameLabel As System.Windows.Forms.Label
    Friend WithEvents NumberOfItemsLabel As System.Windows.Forms.Label
    Friend WithEvents NumberOfItemsText As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents ArtistTagButton As System.Windows.Forms.Button
    Friend WithEvents ArtistTagTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents ArtistTagLabel As System.Windows.Forms.Label
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TrackTagTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TrackTagLabel As System.Windows.Forms.Label
    Friend WithEvents TrackTagButton As System.Windows.Forms.Button
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents PlaylistGeneratorsLabel As System.Windows.Forms.Label
    Friend WithEvents ArtistByUserButton As System.Windows.Forms.Button
    Friend WithEvents ArtistByUserLabel As System.Windows.Forms.Label
    Friend WithEvents ArtistByUserTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents TrackByUserButton As System.Windows.Forms.Button
    Friend WithEvents TrackByUserLabel As System.Windows.Forms.Label
    Friend WithEvents TrackByUserTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents AlbumTagButton As System.Windows.Forms.Button
    Friend WithEvents AlbumTagTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents AlbumTagLabel As System.Windows.Forms.Label
    Friend WithEvents NowPlayingGB As System.Windows.Forms.GroupBox
    Friend WithEvents AlbumYearText As System.Windows.Forms.Label
    Friend WithEvents AlbumYearLabel As System.Windows.Forms.Label
    Friend WithEvents TrackTextLabel As System.Windows.Forms.Label
    Friend WithEvents AlbumTextLabel As System.Windows.Forms.Label
    Friend WithEvents ArtistTextLabel As System.Windows.Forms.Label
    Friend WithEvents TagTrackLabel As System.Windows.Forms.Label
    Friend WithEvents TagAlbumLabel As System.Windows.Forms.Label
    Friend WithEvents TagArtistLabel As System.Windows.Forms.Label
    Friend WithEvents ShuffleCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ArtistFilterGB As System.Windows.Forms.GroupBox
    Friend WithEvents RemoveFilteredOutArtistButton As System.Windows.Forms.Button
    Friend WithEvents AddFilteredOutArtistButton As System.Windows.Forms.Button
    Friend WithEvents ArtistFilterOutTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents FilteredOutArtistsLB As System.Windows.Forms.ListBox

End Class
