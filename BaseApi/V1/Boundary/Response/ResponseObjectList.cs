using System.Collections.Generic;

namespace ArrearsApi.V1.Boundary.Response
{
    //TODO: Rename to represent to object you will be returning eg. ResidentInformationList
    public class ArrearsResponseObjectList
    {
        //TODO: Rename field to match the name of the response object
        //TODO: add xml comments containing information that will be included in the auto generated swagger docs (https://github.com/LBHackney-IT/lbh-base-api/wiki/Controllers-and-Response-Objects)
        public List<ArrearsResponseObject> ArrearsResponseObjects { get; set; }
    }
}
