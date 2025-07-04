using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectPRN222.Models;

public partial class Package
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = null!;

    public int Duration { get; set; }

    public float SalePrice { get; set; }

    public bool Status { get; set; }

    public int SubjectId { get; set; }

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public virtual Subject Subject { get; set; } = null!;
}
//Trigger:
//CREATE TRIGGER trg_UpdateSubjectStatus_Package
//ON Package
//AFTER INSERT, DELETE, UPDATE
//AS
//BEGIN
//    UPDATE s
//    SET s.Status = 
//        CASE
//            WHEN EXISTS(
//                SELECT 1 FROM Lesson l WHERE l.subject_id = s.subject_id
//            ) AND EXISTS(
//                SELECT 1 FROM Package p WHERE p.subject_id = s.subject_id
//            )
//            THEN 1 ELSE 0 
//        END
//    FROM Subject s
//    WHERE s.subject_id IN (
//        SELECT DISTINCT subject_id FROM inserted
//        UNION
//        SELECT DISTINCT subject_id FROM deleted
//    )
//END
