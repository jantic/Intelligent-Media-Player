Imports Microsoft.VisualBasic
Imports System.Xml



Public Class LastFMClient

    'sample api call:  http://ws.audioscrobbler.com/2.0/?method=artist.getsimilar&artist=cher&api_key=b25b959554ed7605
    'Your API Key is e295ea662af320c44101419cb30cfffe and your secret is 5e55b1e71d9aa19bac87f051d8059758

    Private Const baseAPIurl As String = "http://ws.audioscrobbler.com/2.0/?method="
    Private Const similarArtistCall As String = "artist.getsimilar&artist="
    Private Const similarTrackCall_artist As String = "track.getsimilar&artist="
    Private Const similarTrackCall_track As String = "&track="
    Private Const topTagArtistsCall As String = "tag.gettopartists&tag="
    Private Const topTagTracksCall As String = "tag.gettoptracks&tag="
    Private Const topTagAlbumsCall As String = "tag.gettopalbums&tag="
    Private Const topUserArtistsCall As String = "user.gettopartists&user="
    Private Const topUserTracksCall As String = "user.gettoptracks&user="
    Private Const apiTag As String = "&api_key="
    Private Const apiKey As String = "e295ea662af320c44101419cb30cfffe"

    Public Sub New()

    End Sub

    Public Function GetSimilarArtists(ByVal artist As String) As Artist()
        Dim cleanedArtist As String = CleanUpName(artist)
        Dim webserviceCall As String = String.Concat(baseAPIurl, similarArtistCall, cleanedArtist, apiTag, apiKey)
        Dim caller As XmlDocument = New XmlDocument
        caller.Load(webserviceCall)
        Dim root As XmlElement = caller.DocumentElement
        Dim artistNodes As XmlNodeList = root.GetElementsByTagName("artist")

        Dim artistList As ArrayList = New ArrayList

        artistList.Add(New Artist(cleanedArtist, 100)) 'include the artist that is being queried as "similar"

        For Each node As XmlElement In artistNodes
            Dim name As String = node.Item("name").InnerText
            Dim match As Double = Convert.ToDouble(node.Item("match").InnerText)
            artistList.Add(New Artist(name, match))
        Next

        Return artistList.ToArray(GetType(Artist))
    End Function


    Public Function GetSimilarTracks(ByVal artistName As String, ByVal trackName As String) As Track()
        'Sample call: http://ws.audioscrobbler.com/2.0/?method=track.getsimilar&artist=cher&track=believe&api_key=b25...

        ' Example result   
        '  <track>
        '    <name>Ray of Light</name>
        '    <mbid/>
        '    <match>10.95</match>
        '    <url>http://www.last.fm/music/Madonna/_/Ray+of+Light</url>
        '    <streamable fulltrack="0">1</streamable>
        '    <artist>
        '      <name>Madonna</name>
        '      <mbid>79239441-bfd5-4981-a70c-55c3f15c1287</mbid>
        '      <url>http://www.last.fm/music/Madonna</url>
        '    </artist>
        '  </track>

        Dim cleanedArtist As String = CleanUpName(artistName)
        Dim cleanedTrack As String = CleanUpName(trackName)

        Dim webserviceCall As String = String.Concat(baseAPIurl, similarTrackCall_artist, cleanedArtist, _
                similarTrackCall_track, cleanedTrack, apiTag, apiKey)
        Dim caller As XmlDocument = New XmlDocument
        caller.Load(webserviceCall)
        Dim root As XmlElement = caller.DocumentElement
        Dim trackNodes As XmlNodeList = root.GetElementsByTagName("track")

        Dim trackList As ArrayList = New ArrayList

        trackList.Add(New Track(cleanedTrack, 100, New Artist(cleanedArtist, 100)))

        For Each node As XmlElement In trackNodes
            Dim name As String = node.Item("name").InnerText
            Dim match As Double = Convert.ToDouble(node.Item("match").InnerText)
            Dim trackArtist As String = node.Item("artist").Item("name").InnerText
            trackList.Add(New Track(name, match, New Artist(trackArtist, match)))
        Next

        Return trackList.ToArray(GetType(Track))
    End Function

    Public Function GetTopArtistsByTag(ByVal tag As String) As Artist()
        'Sample call:  http://ws.audioscrobbler.com/2.0/?method=tag.gettopartists&tag=disco&api_key=b25b959554ed76058a...

        '<topartists tag="Disco">
        '   <artist rank="">
        '        <name>ABBA</name>
        '        <tagcount>689</tagcount>
        '        <mbid>d87e52c5-bb8d-4da8-b941-9f4928627dc8</mbid>
        '        <url>http://www.last.fm/music/ABBA</url>
        '        <streamable>1</streamable>
        '        <image size="small">...</image>
        '        <image size="medium">...</image>
        '        <image size="large">...</image>
        '    </artist>
        '</topartists>

        Dim cleanedTag As String = CleanUpName(tag)
        Dim webserviceCall As String = String.Concat(baseAPIurl, topTagArtistsCall, cleanedTag, apiTag, apiKey)
        Dim caller As XmlDocument = New XmlDocument
        caller.Load(webserviceCall)
        Dim root As XmlElement = caller.DocumentElement
        Dim artistNodes As XmlNodeList = root.GetElementsByTagName("artist")

        Dim artistList As ArrayList = New ArrayList

        For Each node As XmlElement In artistNodes
            Dim name As String = node.Item("name").InnerText
            Dim match As Double = Convert.ToDouble(node.Item("tagcount").InnerText)
            artistList.Add(New Artist(name, match))
        Next

        Return artistList.ToArray(GetType(Artist))
    End Function


    Public Function GetTopTracksByTag(ByVal tag As String) As Track()
        'Sample call: http://ws.audioscrobbler.com/2.0/?method=tag.gettoptracks&tag=disco&api_key=b25b959554ed76058ac...

        '<toptracks tag="Disco">
        '  <track rank="">
        '    <name>Stayin' Alive</name>
        '    <tagcount>229</tagcount>
        '    <mbid/>
        '    <url>
        '      http://www.last.fm/music/Bee+Gees/_/Stayin'+Alive
        '    </url>
        '    <streamable fulltrack="0">1</streamable>
        '    <artist>
        '      <name>Bee Gees</name>
        '      <mbid>bf0f7e29-dfe1-416c-b5c6-f9ebc19ea810</mbid>
        '      <url>http://www.last.fm/music/Bee+Gees</url>
        '    </artist>
        '    <image size="small">...</image>
        '    <image size="medium">...</image>
        '    <image size="large">...</image>
        '  </track>
        '  ...
        '</toptracks>

        Dim cleanedTag As String = CleanUpName(tag)

        Dim webserviceCall As String = String.Concat(baseAPIurl, topTagTracksCall, cleanedTag, apiTag, apiKey)
        Dim caller As XmlDocument = New XmlDocument
        caller.Load(webserviceCall)
        Dim root As XmlElement = caller.DocumentElement
        Dim trackNodes As XmlNodeList = root.GetElementsByTagName("track")

        Dim trackList As ArrayList = New ArrayList

        For Each node As XmlElement In trackNodes
            Dim name As String = node.Item("name").InnerText
            Dim match As Double = Convert.ToDouble(node.Item("tagcount").InnerText)
            Dim trackArtist As String = node.Item("artist").Item("name").InnerText
            trackList.Add(New Track(name, match, New Artist(trackArtist, match)))
        Next

        Return trackList.ToArray(GetType(Track))
    End Function


    Public Function GetTopAlbumsByTag(ByVal tag As String) As Album()
        'Sample call:  http://ws.audioscrobbler.com/2.0/?method=tag.gettopalbums&tag=disco&api_key=b25b959554ed76058ac...

        '<topalbums tag="Disco">
        '  <album rank="">
        '    <name>Overpowered</name>
        '    <tagcount>104</tagcount>
        '    <mbid/>
        '    <url>
        '      http://www.last.fm/music/Róisín+Murphy/Overpowered
        '    </url>
        '    <artist>
        '      <name>Róisín Murphy</name>
        '      <mbid>4c56405d-ba8e-4283-99c3-1dc95bdd50e7</mbid>
        '      <url>http://www.last.fm/music/Róisín+Murphy</url>
        '    </artist>
        '    <image size="small">...</image>
        '    <image size="medium">...</image>
        '    <image size="large">...</image>
        '  </album>
        '  ...
        '</topalbums>

        Dim cleanedTag As String = CleanUpName(tag)
        Dim webserviceCall As String = String.Concat(baseAPIurl, topTagAlbumsCall, cleanedTag, apiTag, apiKey)
        Dim caller As XmlDocument = New XmlDocument
        caller.Load(webserviceCall)
        Dim root As XmlElement = caller.DocumentElement
        Dim albumNodes As XmlNodeList = root.GetElementsByTagName("album")

        Dim albumList As ArrayList = New ArrayList

        For Each node As XmlElement In albumNodes
            Dim name As String = node.Item("name").InnerText
            Dim tagCount As UInteger = Convert.ToDouble(node.Item("tagcount").InnerText)
            albumList.Add(New Album(name, tagCount))
        Next

        Return albumList.ToArray(GetType(Album))
    End Function


    Public Function GetTopArtistsByUser(ByVal user As String) As Artist()
        'Sample call:  http://ws.audioscrobbler.com/2.0/?method=user.gettopartists&user=rj&api_key=b25b959554ed76058ac...

        '<topartists user="RJ" type="overall">
        '  <artist rank="1">
        '    <name>Dream Theater</name>
        '    <playcount>1337</playcount>
        '    <mbid>28503ab7-8bf2-4666-a7bd-2644bfc7cb1d</mbid>
        '    <url>http://www.last.fm/music/Dream+Theater</url>
        '    <streamable>1</streamable>
        '    <image size="small">...</image>
        '    <image size="medium">...</image>
        '    <image size="large">...</image>
        '  </artist>
        '  ...
        '</topartists>

        Dim cleanedUser As String = CleanUpName(user)
        Dim webserviceCall As String = String.Concat(baseAPIurl, topUserArtistsCall, cleanedUser, apiTag, apiKey)
        Dim caller As XmlDocument = New XmlDocument
        caller.Load(webserviceCall)
        Dim root As XmlElement = caller.DocumentElement
        Dim artistNodes As XmlNodeList = root.GetElementsByTagName("artist")

        Dim artistList As ArrayList = New ArrayList

        For Each node As XmlElement In artistNodes
            Dim name As String = node.Item("name").InnerText
            Dim match As Double = Convert.ToDouble(node.Item("playcount").InnerText)
            artistList.Add(New Artist(name, match))
        Next

        Return artistList.ToArray(GetType(Artist))
    End Function


    Public Function GetTopTracksByUser(ByVal user As String) As Track()
        'Sample call:   http://ws.audioscrobbler.com/2.0/?method=user.gettoptracks&user=rj&api_key=b25b959554ed76058ac2...

        '<toptracks user="RJ" type="overall">  
        '  <track rank="1">
        '    <name>Learning to Live</name>
        '    <playcount>42</playcount>
        '    <mbid/>
        '    <url>
        '      http://www.last.fm/music/Dream+Theater/_/Learning+to+Live
        '    </url>
        '    <streamable fulltrack="0">1</streamable>
        '    <artist>
        '      <name>Dream Theater</name>
        '      <mbid>28503ab7-8bf2-4666-a7bd-2644bfc7cb1d</mbid>
        '      <url>http://www.last.fm/music/Dream+Theater</url>
        '    </artist>
        '    <image size="small">...</image>
        '    <image size="medium">...</image>
        '    <image size="large">...</image>
        '  </track>
        '  ...
        '</toptracks>

        Dim cleanedUser As String = CleanUpName(user)

        Dim webserviceCall As String = String.Concat(baseAPIurl, topUserTracksCall, cleanedUser, apiTag, apiKey)
        Dim caller As XmlDocument = New XmlDocument

        caller.Load(webserviceCall)
        Dim root As XmlElement = caller.DocumentElement
        Dim trackNodes As XmlNodeList = root.GetElementsByTagName("track")

        Dim trackList As ArrayList = New ArrayList

        For Each node As XmlElement In trackNodes
            Dim name As String = node.Item("name").InnerText
            Dim match As Double = Convert.ToDouble(node.Item("playcount").InnerText)
            Dim trackArtist As String = node.Item("artist").Item("name").InnerText
            trackList.Add(New Track(name, match, New Artist(trackArtist, match)))
        Next

        Return trackList.ToArray(GetType(Track))
    End Function


    Private Function CleanUpName(ByVal name As String) As String
        'just get rid of leading and trailing spaces, for now
        Return name.Trim()
    End Function

End Class
