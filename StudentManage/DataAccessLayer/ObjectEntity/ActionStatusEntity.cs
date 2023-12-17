namespace DataAccessLayer.ObjectEntity
{
    public class ActionStatusEntity
    {
        public bool succeed { get; set; }
        public string? error { get; set; }
        public List<int>? objectIds {  get; set; }
        //note: for most object objectIds only contain one key, but for SubjectStudent
        //because it has 2 PK and thus 2 ids, the first id will be subjectId, and the
        //second one will be StudentId
    }
}
