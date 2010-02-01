Public Interface IAlbumInfo
    ReadOnly Property Name As String
    ReadOnly Property Artist As IArtistInfo
    ReadOnly Property PictureLocation As String ' filepath
    ReadOnly Property SmallPictureLocation As String ' filepath
    ReadOnly Property WebLink As String
End Interface
