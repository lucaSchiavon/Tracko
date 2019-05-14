' NOTA: è possibile utilizzare il comando "Rinomina" del menu di scelta rapida per modificare il nome di interfaccia "IService1" nel codice e nel file di configurazione contemporaneamente.
<ServiceContract()>
Public Interface IAPIService

    <OperationContract()>
    <WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.Wrapped, RequestFormat:=WebMessageFormat.Json, ResponseFormat:=WebMessageFormat.Json)>
    Function GetPolicy(ByVal oRequest As ModelContract.Policy.GetPolicy.RequestData) As ModelContract.Policy.GetPolicy.ResponseData

    <OperationContract()>
    <WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.Wrapped, RequestFormat:=WebMessageFormat.Json, ResponseFormat:=WebMessageFormat.Json)>
    Function AddRequest(ByVal oRequest As ModelContract.Contact.AddRequest.RequestData) As ModelContract.Contact.AddRequest.ResponseData

    <OperationContract()>
    <WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.Wrapped, RequestFormat:=WebMessageFormat.Json, ResponseFormat:=WebMessageFormat.Json)>
    Function UpdateMultipleRequestStatus(ByVal oRequest As ModelContract.Contact.UpdateMultipleRequestStatus.RequestData) As ModelContract.Contact.UpdateMultipleRequestStatus.ResponseData

    <OperationContract()>
    <WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.Wrapped, RequestFormat:=WebMessageFormat.Json, ResponseFormat:=WebMessageFormat.Json)>
    Function UpdateRequestStatus(ByVal oRequest As ModelContract.Contact.UpdateRequestStatus.RequestData) As ModelContract.Contact.UpdateRequestStatus.ResponseData

    <OperationContract()>
    <WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.Wrapped, RequestFormat:=WebMessageFormat.Json, ResponseFormat:=WebMessageFormat.Json)>
    Function RetrivePanelLink(ByVal oRequest As ModelContract.Contact.RetrivePanelLink.RequestData) As ModelContract.Contact.RetrivePanelLink.ResponseData

End Interface