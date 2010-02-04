Imports System.Drawing.Image

Public Interface IArtistInfo
    ReadOnly Property Name As String
    ReadOnly Property Summary As String
    ReadOnly Property Biography As String
    ReadOnly Property PictureLocation As String 'filepath
    ReadOnly Property SmallPictureLocation As String ' filepath
    ReadOnly Property SimilarArtists As IArtistNameFace()
    ReadOnly Property WebLink As String
    ReadOnly Property Tags As String()
    ReadOnly Property TopAlbums As IAlbumInfo()
End Interface
