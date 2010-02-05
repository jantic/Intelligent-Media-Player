Imports System.Xml
Imports System.Xml.XPath

Partial Public Class PlaylistManager

    Partial Private Class WMPAttributePlaylistModifier

        Private Class PlaylistModifierAttributeMapping
            Private myID As UInteger
            Private myAttribute As String
            Private myMapToID As UInteger

            Private Sub New(ByVal theId As UInteger, ByVal theAttribute As String, ByVal theMapToID As UInteger)
                myID = theId
                myAttribute = theAttribute
                myMapToID = theMapToID
            End Sub

            Public Shared Function LoadAttributeMappings(ByRef liason As PlaylistModifierUILiason) As PlaylistModifierAttributeMapping()
                Dim modifierFile As XmlDocument = New XmlDocument()
                modifierFile.Load(liason.FilePath)

                Dim mappings As ArrayList = New ArrayList
                Dim index As UInteger = 0
                Dim attribute As String = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//Method/MediaAttributeMatch/AttributeMappings/Mapping/WMP_Attribute", index)

                While (Not (attribute Is Nothing))
                    Dim ID As UInteger = UInteger.Parse(WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//Method/MediaAttributeMatch/AttributeMappings/Mapping/ID", index))
                    Dim InputID As UInteger = UInteger.Parse(WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//Method/MediaAttributeMatch/AttributeMappings/Mapping/InputID", index))

                    mappings.Add(New PlaylistModifierAttributeMapping(ID, attribute, InputID))

                    index += 1
                    attribute = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//Method/MediaAttributeMatch/AttributeMappings/Mapping/WMP_Attribute", index)
                End While

                Return mappings.ToArray(GetType(PlaylistModifierAttributeMapping))
            End Function

            Public ReadOnly Property ID As UInteger
                Get
                    Return myID
                End Get
            End Property

            Public ReadOnly Property Attribute As String
                Get
                    Return myAttribute
                End Get
            End Property

            Public ReadOnly Property MapToId As UInteger
                Get
                    Return myMapToID
                End Get
            End Property

            Public Function GetMatchingInputValue(ByRef inputValues As PlaylistModifierInput()) As String
                For Each input As PlaylistModifierInput In inputValues
                    If (input.ID = myMapToID) Then
                        Return input.Value
                    End If
                Next

                Return ""
            End Function


        End Class
    End Class
End Class