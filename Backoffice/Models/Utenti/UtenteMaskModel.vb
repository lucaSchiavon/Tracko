Imports System.ComponentModel.DataAnnotations
Namespace Model.Utenti

    Public Class UtenteMaskModel

        Public Property action As String

        Public Property Id As Integer

        <Required>
        Public Property Cognome As String

        <Required>
        Property Nome As String

        <Required>
        <Remote("CheckUsername", "Utenti", AdditionalFields:="Id", HttpMethod:="POST", ErrorMessage:="Username già presente")>
        Property Username As String

        <RegularExpression("^(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z]).{8,}$", ErrorMessage:="La password deve contenere una lettera maiuscole, un numero, una lettera minuscola e deve essere lunga almeno otto caratteri")>
        <DataType(DataType.Password)>
        Property Password As String

        <DataType(DataType.Password)>
        <Compare("Password", ErrorMessage:="Le password non coincidono")>
        Property ConfermaPassword As String

        <Required>
        <DataType(DataType.EmailAddress)>
        <EmailAddress>
        <Remote("CheckEmail", "Utenti", AdditionalFields:="Id", HttpMethod:="POST", ErrorMessage:="Email già presente")>
        Property Email As String

        Public Property ClienteIsSettable As Boolean

        Public Property ClienteId As Integer?

        Public Property ClienteList As SelectList

        Property ErrorMessage As String = String.Empty

        Public Property goBackLink As String = String.Empty

    End Class

End Namespace