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
        Me.NumberOfItemsLabel = New System.Windows.Forms.Label()
        Me.NumberOfItemsText = New System.Windows.Forms.Label()
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
        Me.PlaylistModifierGB = New System.Windows.Forms.GroupBox()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.ActivePlaylistModifiersLB = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.AvailablePlaylistModifiersLB = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ActivePlaylistModifiersLabel = New System.Windows.Forms.Label()
        Me.AvailablePlaylistModifiersLabel = New System.Windows.Forms.Label()
        Me.GeneratePlaylistButton = New System.Windows.Forms.Button()
        Me.ModifierInputGB = New System.Windows.Forms.GroupBox()
        Me.RemoveRb = New System.Windows.Forms.RadioButton()
        Me.AddRB = New System.Windows.Forms.RadioButton()
        Me.FilterRB = New System.Windows.Forms.RadioButton()
        Me.Input3Label = New System.Windows.Forms.Label()
        Me.Input2Label = New System.Windows.Forms.Label()
        Me.Input1Label = New System.Windows.Forms.Label()
        Me.PlaylistModifierInput2 = New System.Windows.Forms.MaskedTextBox()
        Me.PlaylistModifierInput3 = New System.Windows.Forms.MaskedTextBox()
        Me.PlaylistModifierInput1 = New System.Windows.Forms.MaskedTextBox()
        Me.RemoveModifierButton = New System.Windows.Forms.Button()
        Me.AddModifierButton = New System.Windows.Forms.Button()
        Me.ArtistPictureBox = New System.Windows.Forms.PictureBox()
        Me.FullBioTB = New System.Windows.Forms.WebBrowser()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.BiographyTab = New System.Windows.Forms.TabPage()
        Me.SimilarArtistsTab = New System.Windows.Forms.TabPage()
        Me.SimilarArtistsLV = New System.Windows.Forms.ListView()
        Me.TopAlbumsTab = New System.Windows.Forms.TabPage()
        Me.TopAlbumsLV = New System.Windows.Forms.ListView()
        Me.TagsTab = New System.Windows.Forms.TabPage()
        Me.Tag5 = New System.Windows.Forms.Label()
        Me.Tag4 = New System.Windows.Forms.Label()
        Me.Tag3 = New System.Windows.Forms.Label()
        Me.Tag2 = New System.Windows.Forms.Label()
        Me.Tag1 = New System.Windows.Forms.Label()
        Me.NowPlayingGB.SuspendLayout()
        Me.PlaylistModifierGB.SuspendLayout()
        Me.ModifierInputGB.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.BiographyTab.SuspendLayout()
        Me.SimilarArtistsTab.SuspendLayout()
        Me.TopAlbumsTab.SuspendLayout()
        Me.TagsTab.SuspendLayout()
        Me.SuspendLayout()
        '
        'AxWindowsMediaPlayer1
        '
        Me.AxWindowsMediaPlayer1.Enabled = True
        Me.AxWindowsMediaPlayer1.Location = New System.Drawing.Point(285, 12)
        Me.AxWindowsMediaPlayer1.Name = "AxWindowsMediaPlayer1"
        Me.AxWindowsMediaPlayer1.OcxState = CType(resources.GetObject("AxWindowsMediaPlayer1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxWindowsMediaPlayer1.Size = New System.Drawing.Size(264, 181)
        Me.AxWindowsMediaPlayer1.TabIndex = 0
        '
        'PlaylistBox
        '
        Me.PlaylistBox.FormattingEnabled = True
        Me.PlaylistBox.Location = New System.Drawing.Point(12, 222)
        Me.PlaylistBox.Name = "PlaylistBox"
        Me.PlaylistBox.Size = New System.Drawing.Size(537, 290)
        Me.PlaylistBox.TabIndex = 1
        '
        'FilteredPlaylistLabel
        '
        Me.FilteredPlaylistLabel.AutoSize = True
        Me.FilteredPlaylistLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FilteredPlaylistLabel.Location = New System.Drawing.Point(12, 206)
        Me.FilteredPlaylistLabel.Name = "FilteredPlaylistLabel"
        Me.FilteredPlaylistLabel.Size = New System.Drawing.Size(76, 13)
        Me.FilteredPlaylistLabel.TabIndex = 4
        Me.FilteredPlaylistLabel.Text = "Filtered Playlist"
        '
        'NumberOfItemsLabel
        '
        Me.NumberOfItemsLabel.AutoSize = True
        Me.NumberOfItemsLabel.Location = New System.Drawing.Point(9, 515)
        Me.NumberOfItemsLabel.Name = "NumberOfItemsLabel"
        Me.NumberOfItemsLabel.Size = New System.Drawing.Size(87, 13)
        Me.NumberOfItemsLabel.TabIndex = 17
        Me.NumberOfItemsLabel.Text = "Number of Items:"
        '
        'NumberOfItemsText
        '
        Me.NumberOfItemsText.AutoSize = True
        Me.NumberOfItemsText.Location = New System.Drawing.Point(108, 515)
        Me.NumberOfItemsText.Name = "NumberOfItemsText"
        Me.NumberOfItemsText.Size = New System.Drawing.Size(0, 13)
        Me.NumberOfItemsText.TabIndex = 18
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
        Me.NowPlayingGB.Location = New System.Drawing.Point(12, 12)
        Me.NowPlayingGB.Name = "NowPlayingGB"
        Me.NowPlayingGB.Size = New System.Drawing.Size(265, 181)
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
        Me.ShuffleCheckBox.Location = New System.Drawing.Point(6, 131)
        Me.ShuffleCheckBox.Name = "ShuffleCheckBox"
        Me.ShuffleCheckBox.Size = New System.Drawing.Size(59, 17)
        Me.ShuffleCheckBox.TabIndex = 17
        Me.ShuffleCheckBox.Text = "Shuffle"
        Me.ShuffleCheckBox.UseVisualStyleBackColor = True
        '
        'PlaylistModifierGB
        '
        Me.PlaylistModifierGB.Controls.Add(Me.SaveButton)
        Me.PlaylistModifierGB.Controls.Add(Me.ActivePlaylistModifiersLB)
        Me.PlaylistModifierGB.Controls.Add(Me.AvailablePlaylistModifiersLB)
        Me.PlaylistModifierGB.Controls.Add(Me.ActivePlaylistModifiersLabel)
        Me.PlaylistModifierGB.Controls.Add(Me.AvailablePlaylistModifiersLabel)
        Me.PlaylistModifierGB.Controls.Add(Me.GeneratePlaylistButton)
        Me.PlaylistModifierGB.Controls.Add(Me.ModifierInputGB)
        Me.PlaylistModifierGB.Controls.Add(Me.RemoveModifierButton)
        Me.PlaylistModifierGB.Controls.Add(Me.AddModifierButton)
        Me.PlaylistModifierGB.Location = New System.Drawing.Point(12, 543)
        Me.PlaylistModifierGB.Name = "PlaylistModifierGB"
        Me.PlaylistModifierGB.Size = New System.Drawing.Size(1042, 225)
        Me.PlaylistModifierGB.TabIndex = 21
        Me.PlaylistModifierGB.TabStop = False
        Me.PlaylistModifierGB.Text = "Playlist Modification"
        '
        'SaveButton
        '
        Me.SaveButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveButton.Image = Global.IntelligentMediaPlayer.My.Resources.Resources.SaveHQ
        Me.SaveButton.Location = New System.Drawing.Point(990, 109)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(40, 40)
        Me.SaveButton.TabIndex = 24
        Me.SaveButton.Text = "-"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'ActivePlaylistModifiersLB
        '
        Me.ActivePlaylistModifiersLB.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
        Me.ActivePlaylistModifiersLB.FullRowSelect = True
        Me.ActivePlaylistModifiersLB.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ActivePlaylistModifiersLB.HideSelection = False
        Me.ActivePlaylistModifiersLB.Location = New System.Drawing.Point(554, 40)
        Me.ActivePlaylistModifiersLB.MultiSelect = False
        Me.ActivePlaylistModifiersLB.Name = "ActivePlaylistModifiersLB"
        Me.ActivePlaylistModifiersLB.ShowGroups = False
        Me.ActivePlaylistModifiersLB.Size = New System.Drawing.Size(430, 173)
        Me.ActivePlaylistModifiersLB.TabIndex = 23
        Me.ActivePlaylistModifiersLB.UseCompatibleStateImageBehavior = False
        Me.ActivePlaylistModifiersLB.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = ""
        Me.ColumnHeader2.Width = 400
        '
        'AvailablePlaylistModifiersLB
        '
        Me.AvailablePlaylistModifiersLB.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.AvailablePlaylistModifiersLB.FullRowSelect = True
        Me.AvailablePlaylistModifiersLB.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.AvailablePlaylistModifiersLB.HideSelection = False
        Me.AvailablePlaylistModifiersLB.Location = New System.Drawing.Point(6, 40)
        Me.AvailablePlaylistModifiersLB.MultiSelect = False
        Me.AvailablePlaylistModifiersLB.Name = "AvailablePlaylistModifiersLB"
        Me.AvailablePlaylistModifiersLB.ShowGroups = False
        Me.AvailablePlaylistModifiersLB.Size = New System.Drawing.Size(257, 173)
        Me.AvailablePlaylistModifiersLB.TabIndex = 22
        Me.AvailablePlaylistModifiersLB.UseCompatibleStateImageBehavior = False
        Me.AvailablePlaylistModifiersLB.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = ""
        Me.ColumnHeader1.Width = 193
        '
        'ActivePlaylistModifiersLabel
        '
        Me.ActivePlaylistModifiersLabel.AutoSize = True
        Me.ActivePlaylistModifiersLabel.Location = New System.Drawing.Point(551, 24)
        Me.ActivePlaylistModifiersLabel.Name = "ActivePlaylistModifiersLabel"
        Me.ActivePlaylistModifiersLabel.Size = New System.Drawing.Size(117, 13)
        Me.ActivePlaylistModifiersLabel.TabIndex = 14
        Me.ActivePlaylistModifiersLabel.Text = "Active Playlist Modifiers"
        '
        'AvailablePlaylistModifiersLabel
        '
        Me.AvailablePlaylistModifiersLabel.AutoSize = True
        Me.AvailablePlaylistModifiersLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AvailablePlaylistModifiersLabel.Location = New System.Drawing.Point(6, 24)
        Me.AvailablePlaylistModifiersLabel.Name = "AvailablePlaylistModifiersLabel"
        Me.AvailablePlaylistModifiersLabel.Size = New System.Drawing.Size(130, 13)
        Me.AvailablePlaylistModifiersLabel.TabIndex = 13
        Me.AvailablePlaylistModifiersLabel.Text = "Available Playlist Modifiers"
        '
        'GeneratePlaylistButton
        '
        Me.GeneratePlaylistButton.BackColor = System.Drawing.SystemColors.Control
        Me.GeneratePlaylistButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GeneratePlaylistButton.ForeColor = System.Drawing.SystemColors.Control
        Me.GeneratePlaylistButton.Image = Global.IntelligentMediaPlayer.My.Resources.Resources.PlaylistHQ
        Me.GeneratePlaylistButton.Location = New System.Drawing.Point(990, 173)
        Me.GeneratePlaylistButton.Name = "GeneratePlaylistButton"
        Me.GeneratePlaylistButton.Size = New System.Drawing.Size(40, 40)
        Me.GeneratePlaylistButton.TabIndex = 11
        Me.GeneratePlaylistButton.UseVisualStyleBackColor = False
        '
        'ModifierInputGB
        '
        Me.ModifierInputGB.Controls.Add(Me.RemoveRb)
        Me.ModifierInputGB.Controls.Add(Me.AddRB)
        Me.ModifierInputGB.Controls.Add(Me.FilterRB)
        Me.ModifierInputGB.Controls.Add(Me.Input3Label)
        Me.ModifierInputGB.Controls.Add(Me.Input2Label)
        Me.ModifierInputGB.Controls.Add(Me.Input1Label)
        Me.ModifierInputGB.Controls.Add(Me.PlaylistModifierInput2)
        Me.ModifierInputGB.Controls.Add(Me.PlaylistModifierInput3)
        Me.ModifierInputGB.Controls.Add(Me.PlaylistModifierInput1)
        Me.ModifierInputGB.Location = New System.Drawing.Point(269, 33)
        Me.ModifierInputGB.Name = "ModifierInputGB"
        Me.ModifierInputGB.Size = New System.Drawing.Size(234, 180)
        Me.ModifierInputGB.TabIndex = 10
        Me.ModifierInputGB.TabStop = False
        Me.ModifierInputGB.Text = "Modifier Input"
        '
        'RemoveRb
        '
        Me.RemoveRb.AutoSize = True
        Me.RemoveRb.Location = New System.Drawing.Point(163, 157)
        Me.RemoveRb.Name = "RemoveRb"
        Me.RemoveRb.Size = New System.Drawing.Size(65, 17)
        Me.RemoveRb.TabIndex = 18
        Me.RemoveRb.TabStop = True
        Me.RemoveRb.Text = "Remove"
        Me.RemoveRb.UseVisualStyleBackColor = True
        '
        'AddRB
        '
        Me.AddRB.AutoSize = True
        Me.AddRB.Location = New System.Drawing.Point(92, 157)
        Me.AddRB.Name = "AddRB"
        Me.AddRB.Size = New System.Drawing.Size(44, 17)
        Me.AddRB.TabIndex = 17
        Me.AddRB.TabStop = True
        Me.AddRB.Text = "Add"
        Me.AddRB.UseVisualStyleBackColor = True
        '
        'FilterRB
        '
        Me.FilterRB.AutoSize = True
        Me.FilterRB.Location = New System.Drawing.Point(12, 157)
        Me.FilterRB.Name = "FilterRB"
        Me.FilterRB.Size = New System.Drawing.Size(47, 17)
        Me.FilterRB.TabIndex = 16
        Me.FilterRB.TabStop = True
        Me.FilterRB.Text = "Filter"
        Me.FilterRB.UseVisualStyleBackColor = True
        '
        'Input3Label
        '
        Me.Input3Label.AutoSize = True
        Me.Input3Label.Location = New System.Drawing.Point(9, 103)
        Me.Input3Label.Name = "Input3Label"
        Me.Input3Label.Size = New System.Drawing.Size(40, 13)
        Me.Input3Label.TabIndex = 15
        Me.Input3Label.Text = "Input 3"
        '
        'Input2Label
        '
        Me.Input2Label.AutoSize = True
        Me.Input2Label.Location = New System.Drawing.Point(9, 67)
        Me.Input2Label.Name = "Input2Label"
        Me.Input2Label.Size = New System.Drawing.Size(40, 13)
        Me.Input2Label.TabIndex = 14
        Me.Input2Label.Text = "Input 2"
        '
        'Input1Label
        '
        Me.Input1Label.AutoSize = True
        Me.Input1Label.Location = New System.Drawing.Point(9, 28)
        Me.Input1Label.Name = "Input1Label"
        Me.Input1Label.Size = New System.Drawing.Size(40, 13)
        Me.Input1Label.TabIndex = 13
        Me.Input1Label.Text = "Input 1"
        '
        'PlaylistModifierInput2
        '
        Me.PlaylistModifierInput2.Location = New System.Drawing.Point(95, 64)
        Me.PlaylistModifierInput2.Name = "PlaylistModifierInput2"
        Me.PlaylistModifierInput2.Size = New System.Drawing.Size(130, 20)
        Me.PlaylistModifierInput2.TabIndex = 12
        '
        'PlaylistModifierInput3
        '
        Me.PlaylistModifierInput3.Location = New System.Drawing.Point(95, 100)
        Me.PlaylistModifierInput3.Name = "PlaylistModifierInput3"
        Me.PlaylistModifierInput3.Size = New System.Drawing.Size(130, 20)
        Me.PlaylistModifierInput3.TabIndex = 11
        '
        'PlaylistModifierInput1
        '
        Me.PlaylistModifierInput1.Location = New System.Drawing.Point(95, 25)
        Me.PlaylistModifierInput1.Name = "PlaylistModifierInput1"
        Me.PlaylistModifierInput1.Size = New System.Drawing.Size(130, 20)
        Me.PlaylistModifierInput1.TabIndex = 10
        '
        'RemoveModifierButton
        '
        Me.RemoveModifierButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RemoveModifierButton.Image = Global.IntelligentMediaPlayer.My.Resources.Resources.MinusHQ
        Me.RemoveModifierButton.Location = New System.Drawing.Point(990, 48)
        Me.RemoveModifierButton.Name = "RemoveModifierButton"
        Me.RemoveModifierButton.Size = New System.Drawing.Size(40, 40)
        Me.RemoveModifierButton.TabIndex = 3
        Me.RemoveModifierButton.UseVisualStyleBackColor = True
        '
        'AddModifierButton
        '
        Me.AddModifierButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddModifierButton.Image = Global.IntelligentMediaPlayer.My.Resources.Resources.PlusHQ
        Me.AddModifierButton.Location = New System.Drawing.Point(509, 48)
        Me.AddModifierButton.Name = "AddModifierButton"
        Me.AddModifierButton.Size = New System.Drawing.Size(40, 40)
        Me.AddModifierButton.TabIndex = 2
        Me.AddModifierButton.UseVisualStyleBackColor = True
        '
        'ArtistPictureBox
        '
        Me.ArtistPictureBox.BackColor = System.Drawing.SystemColors.Desktop
        Me.ArtistPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ArtistPictureBox.ErrorImage = Nothing
        Me.ArtistPictureBox.ImageLocation = ""
        Me.ArtistPictureBox.InitialImage = Nothing
        Me.ArtistPictureBox.Location = New System.Drawing.Point(566, 12)
        Me.ArtistPictureBox.Name = "ArtistPictureBox"
        Me.ArtistPictureBox.Size = New System.Drawing.Size(488, 267)
        Me.ArtistPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ArtistPictureBox.TabIndex = 22
        Me.ArtistPictureBox.TabStop = False
        '
        'FullBioTB
        '
        Me.FullBioTB.Location = New System.Drawing.Point(6, 6)
        Me.FullBioTB.MinimumSize = New System.Drawing.Size(20, 20)
        Me.FullBioTB.Name = "FullBioTB"
        Me.FullBioTB.Size = New System.Drawing.Size(468, 188)
        Me.FullBioTB.TabIndex = 29
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.BiographyTab)
        Me.TabControl1.Controls.Add(Me.SimilarArtistsTab)
        Me.TabControl1.Controls.Add(Me.TopAlbumsTab)
        Me.TabControl1.Controls.Add(Me.TagsTab)
        Me.TabControl1.Location = New System.Drawing.Point(566, 286)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(488, 226)
        Me.TabControl1.TabIndex = 30
        '
        'BiographyTab
        '
        Me.BiographyTab.Controls.Add(Me.FullBioTB)
        Me.BiographyTab.Location = New System.Drawing.Point(4, 22)
        Me.BiographyTab.Name = "BiographyTab"
        Me.BiographyTab.Padding = New System.Windows.Forms.Padding(3)
        Me.BiographyTab.Size = New System.Drawing.Size(480, 200)
        Me.BiographyTab.TabIndex = 0
        Me.BiographyTab.Text = "Biography"
        Me.BiographyTab.UseVisualStyleBackColor = True
        '
        'SimilarArtistsTab
        '
        Me.SimilarArtistsTab.Controls.Add(Me.SimilarArtistsLV)
        Me.SimilarArtistsTab.Location = New System.Drawing.Point(4, 22)
        Me.SimilarArtistsTab.Name = "SimilarArtistsTab"
        Me.SimilarArtistsTab.Size = New System.Drawing.Size(480, 200)
        Me.SimilarArtistsTab.TabIndex = 2
        Me.SimilarArtistsTab.Text = "Simlar Artists"
        Me.SimilarArtistsTab.UseVisualStyleBackColor = True
        '
        'SimilarArtistsLV
        '
        Me.SimilarArtistsLV.FullRowSelect = True
        Me.SimilarArtistsLV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.SimilarArtistsLV.HideSelection = False
        Me.SimilarArtistsLV.Location = New System.Drawing.Point(3, 3)
        Me.SimilarArtistsLV.MultiSelect = False
        Me.SimilarArtistsLV.Name = "SimilarArtistsLV"
        Me.SimilarArtistsLV.RightToLeftLayout = True
        Me.SimilarArtistsLV.ShowGroups = False
        Me.SimilarArtistsLV.Size = New System.Drawing.Size(474, 194)
        Me.SimilarArtistsLV.TabIndex = 24
        Me.SimilarArtistsLV.UseCompatibleStateImageBehavior = False
        '
        'TopAlbumsTab
        '
        Me.TopAlbumsTab.Controls.Add(Me.TopAlbumsLV)
        Me.TopAlbumsTab.Location = New System.Drawing.Point(4, 22)
        Me.TopAlbumsTab.Name = "TopAlbumsTab"
        Me.TopAlbumsTab.Size = New System.Drawing.Size(480, 200)
        Me.TopAlbumsTab.TabIndex = 3
        Me.TopAlbumsTab.Text = "Top Albums"
        Me.TopAlbumsTab.UseVisualStyleBackColor = True
        '
        'TopAlbumsLV
        '
        Me.TopAlbumsLV.FullRowSelect = True
        Me.TopAlbumsLV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.TopAlbumsLV.HideSelection = False
        Me.TopAlbumsLV.Location = New System.Drawing.Point(3, 3)
        Me.TopAlbumsLV.MultiSelect = False
        Me.TopAlbumsLV.Name = "TopAlbumsLV"
        Me.TopAlbumsLV.RightToLeftLayout = True
        Me.TopAlbumsLV.ShowGroups = False
        Me.TopAlbumsLV.Size = New System.Drawing.Size(474, 194)
        Me.TopAlbumsLV.TabIndex = 23
        Me.TopAlbumsLV.UseCompatibleStateImageBehavior = False
        '
        'TagsTab
        '
        Me.TagsTab.Controls.Add(Me.Tag5)
        Me.TagsTab.Controls.Add(Me.Tag4)
        Me.TagsTab.Controls.Add(Me.Tag3)
        Me.TagsTab.Controls.Add(Me.Tag2)
        Me.TagsTab.Controls.Add(Me.Tag1)
        Me.TagsTab.Location = New System.Drawing.Point(4, 22)
        Me.TagsTab.Name = "TagsTab"
        Me.TagsTab.Padding = New System.Windows.Forms.Padding(3)
        Me.TagsTab.Size = New System.Drawing.Size(480, 200)
        Me.TagsTab.TabIndex = 1
        Me.TagsTab.Text = "Tags"
        Me.TagsTab.UseVisualStyleBackColor = True
        '
        'Tag5
        '
        Me.Tag5.AutoSize = True
        Me.Tag5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tag5.Location = New System.Drawing.Point(6, 143)
        Me.Tag5.Name = "Tag5"
        Me.Tag5.Size = New System.Drawing.Size(57, 20)
        Me.Tag5.TabIndex = 4
        Me.Tag5.Text = "Label2"
        '
        'Tag4
        '
        Me.Tag4.AutoSize = True
        Me.Tag4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tag4.Location = New System.Drawing.Point(7, 113)
        Me.Tag4.Name = "Tag4"
        Me.Tag4.Size = New System.Drawing.Size(57, 20)
        Me.Tag4.TabIndex = 3
        Me.Tag4.Text = "Label1"
        '
        'Tag3
        '
        Me.Tag3.AutoSize = True
        Me.Tag3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tag3.Location = New System.Drawing.Point(6, 82)
        Me.Tag3.Name = "Tag3"
        Me.Tag3.Size = New System.Drawing.Size(57, 20)
        Me.Tag3.TabIndex = 2
        Me.Tag3.Text = "Label1"
        '
        'Tag2
        '
        Me.Tag2.AutoSize = True
        Me.Tag2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tag2.Location = New System.Drawing.Point(7, 52)
        Me.Tag2.Name = "Tag2"
        Me.Tag2.Size = New System.Drawing.Size(57, 20)
        Me.Tag2.TabIndex = 1
        Me.Tag2.Text = "Label1"
        '
        'Tag1
        '
        Me.Tag1.AutoSize = True
        Me.Tag1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tag1.Location = New System.Drawing.Point(7, 20)
        Me.Tag1.Name = "Tag1"
        Me.Tag1.Size = New System.Drawing.Size(57, 20)
        Me.Tag1.TabIndex = 0
        Me.Tag1.Text = "Label1"
        '
        'MainInterface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1064, 777)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ArtistPictureBox)
        Me.Controls.Add(Me.PlaylistModifierGB)
        Me.Controls.Add(Me.NowPlayingGB)
        Me.Controls.Add(Me.NumberOfItemsText)
        Me.Controls.Add(Me.NumberOfItemsLabel)
        Me.Controls.Add(Me.FilteredPlaylistLabel)
        Me.Controls.Add(Me.PlaylistBox)
        Me.Controls.Add(Me.AxWindowsMediaPlayer1)
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.MaximumSize = New System.Drawing.Size(1080, 815)
        Me.MinimumSize = New System.Drawing.Size(1080, 815)
        Me.Name = "MainInterface"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Intelligent Media Player"
        Me.NowPlayingGB.ResumeLayout(False)
        Me.NowPlayingGB.PerformLayout()
        Me.PlaylistModifierGB.ResumeLayout(False)
        Me.PlaylistModifierGB.PerformLayout()
        Me.ModifierInputGB.ResumeLayout(False)
        Me.ModifierInputGB.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.BiographyTab.ResumeLayout(False)
        Me.SimilarArtistsTab.ResumeLayout(False)
        Me.TopAlbumsTab.ResumeLayout(False)
        Me.TagsTab.ResumeLayout(False)
        Me.TagsTab.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AxWindowsMediaPlayer1 As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents PlaylistBox As System.Windows.Forms.ListBox
    Friend WithEvents FilteredPlaylistLabel As System.Windows.Forms.Label
    Friend WithEvents NumberOfItemsLabel As System.Windows.Forms.Label
    Friend WithEvents NumberOfItemsText As System.Windows.Forms.Label
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
    Friend WithEvents PlaylistModifierGB As System.Windows.Forms.GroupBox
    Friend WithEvents RemoveModifierButton As System.Windows.Forms.Button
    Friend WithEvents AddModifierButton As System.Windows.Forms.Button
    Friend WithEvents ModifierInputGB As System.Windows.Forms.GroupBox
    Friend WithEvents Input3Label As System.Windows.Forms.Label
    Friend WithEvents Input2Label As System.Windows.Forms.Label
    Friend WithEvents Input1Label As System.Windows.Forms.Label
    Friend WithEvents PlaylistModifierInput2 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents PlaylistModifierInput3 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents PlaylistModifierInput1 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents GeneratePlaylistButton As System.Windows.Forms.Button
    Friend WithEvents ActivePlaylistModifiersLabel As System.Windows.Forms.Label
    Friend WithEvents AvailablePlaylistModifiersLabel As System.Windows.Forms.Label
    Friend WithEvents RemoveRb As System.Windows.Forms.RadioButton
    Friend WithEvents AddRB As System.Windows.Forms.RadioButton
    Friend WithEvents FilterRB As System.Windows.Forms.RadioButton
    Friend WithEvents AvailablePlaylistModifiersLB As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ActivePlaylistModifiersLB As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ArtistPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents FullBioTB As System.Windows.Forms.WebBrowser
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents BiographyTab As System.Windows.Forms.TabPage
    Friend WithEvents TagsTab As System.Windows.Forms.TabPage
    Friend WithEvents Tag5 As System.Windows.Forms.Label
    Friend WithEvents Tag4 As System.Windows.Forms.Label
    Friend WithEvents Tag3 As System.Windows.Forms.Label
    Friend WithEvents Tag2 As System.Windows.Forms.Label
    Friend WithEvents Tag1 As System.Windows.Forms.Label
    Friend WithEvents TopAlbumsTab As System.Windows.Forms.TabPage
    Friend WithEvents TopAlbumsLV As System.Windows.Forms.ListView
    Friend WithEvents SimilarArtistsTab As System.Windows.Forms.TabPage
    Friend WithEvents SimilarArtistsLV As System.Windows.Forms.ListView
    Friend WithEvents SaveButton As System.Windows.Forms.Button

End Class
