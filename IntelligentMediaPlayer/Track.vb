' Example result   
'   <name>Ray of Light</name>
'    <mbid/>
'    <match>10.95</match>
'    <url>http://www.last.fm/music/Madonna/_/Ray+of+Light</url>
'    <streamable fulltrack="0">1</streamable>
'    <artist>
'      <name>Madonna</name>
'      <mbid>79239441-bfd5-4981-a70c-55c3f15c1287</mbid>
'      <url>http://www.last.fm/music/Madonna</url>
'    </artist>



Public Class Track

    Private myName As String
    Private myMatchFactor As Double
    Private myArtist As Artist

    Public Sub New(ByVal name As String, ByVal matchFactor As Double, ByVal theArtist As Artist)
        myName = name
        myMatchFactor = matchFactor
        myArtist = theArtist
    End Sub

    Public ReadOnly Property Name() As String
        Get
            Return myName
        End Get
    End Property

    Public ReadOnly Property MatchFactor() As Double
        Get
            Return myMatchFactor
        End Get
    End Property

    Public ReadOnly Property TheArtist() As Artist
        Get
            Return myArtist
        End Get
    End Property
End Class
