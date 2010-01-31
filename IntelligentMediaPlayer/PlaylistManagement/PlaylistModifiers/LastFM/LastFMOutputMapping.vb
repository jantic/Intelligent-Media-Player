Imports System.Xml
Imports System.Xml.XPath

Partial Public Class PlaylistManager

    Private Class LastFMOutputMapping

        Private myAttribute As String
        Private myXPath As String = ""
        Private myID As UInteger

        Private Sub New(ByVal theAttribute As String, ByVal theXPath As String, ByVal theID As UInteger)
            myAttribute = theAttribute
            myXPath = theXPath
            myID = theID
        End Sub

        Public ReadOnly Property Attribute As String
            Get
                Return myAttribute
            End Get
        End Property

        Public ReadOnly Property ID As UInteger
            Get
                Return myID
            End Get
        End Property

        Public Function ExtractOutputValue(ByVal index As UInteger, ByRef resultXML As XmlDocument) As String
            Return WebServiceClient.GetClient.XMLGetValueAt(resultXML, myXPath, index)
        End Function

        Public Shared Function LoadResultMappings(ByRef liason As PlaylistModifierUILiason) As LastFMOutputMapping()

            Dim modifierFile As XmlDocument = New XmlDocument()
            modifierFile.Load(liason.FilePath)

            Dim mappings As ArrayList = New ArrayList
            Dim index As UInteger = 0
            Dim attribute As String = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//Response/Outputs/Output/WMP_Attribute", index)

            While (Not (attribute Is Nothing))
                Dim ID As UInteger = UInteger.Parse(WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//Response/Outputs/Output/ID", index))
                Dim xPathQuery As String = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//Response/Outputs/Output/xPath", index)

                mappings.Add(New LastFMOutputMapping(attribute, xPathQuery, ID))

                index += 1
                attribute = WebServiceClient.GetClient.XMLGetValueAt(modifierFile, "//Response/Outputs/Output/WMP_Attribute", index)
            End While

            Return mappings.ToArray(GetType(LastFMOutputMapping))
        End Function


    End Class
End Class
