namespace UniversityCourseAndResultManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UCv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassRoomAllocations",
                c => new
                    {
                        ClassRoomAllocationId = c.Int(nullable: false, identity: true),
                        TimeFrom = c.String(nullable: false, maxLength: 50, unicode: false),
                        TimeTo = c.String(nullable: false, maxLength: 50, unicode: false),
                        Status = c.String(maxLength: 50, unicode: false),
                        DepartmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClassRoomAllocationId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: false)
                .ForeignKey("dbo.SevenDayWeeks", t => t.DayId, cascadeDelete: false)
                .Index(t => t.DepartmentId)
                .Index(t => t.CourseId)
                .Index(t => t.RoomId)
                .Index(t => t.DayId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseCode = c.String(nullable: false, maxLength: 15, unicode: false),
                        CoursName = c.String(nullable: false, maxLength: 50, unicode: false),
                        CourseCredit = c.Double(nullable: false),
                        CourseDescription = c.String(nullable: false, maxLength: 150, unicode: false),
                        DepartmentId = c.Int(nullable: false),
                        SemesterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.SemesterId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentCode = c.String(nullable: false, maxLength: 7, unicode: false),
                        DepartmentName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        SemesterId = c.Int(nullable: false, identity: true),
                        SemesterName = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.SemesterId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomNo = c.String(),
                        RoomName = c.String(),
                        RoomLocation = c.String(),
                    })
                .PrimaryKey(t => t.RoomId);
            
            CreateTable(
                "dbo.SevenDayWeeks",
                c => new
                    {
                        DayId = c.Int(nullable: false, identity: true),
                        DayCode = c.String(),
                        DayName = c.String(),
                    })
                .PrimaryKey(t => t.DayId);
            
            CreateTable(
                "dbo.CourseAssignToTeachers",
                c => new
                    {
                        CourseAssignId = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        Status = c.String(maxLength: 50, unicode: false),
                        TeacherId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseAssignId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: false)
                .Index(t => t.DepartmentId)
                .Index(t => t.TeacherId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        TeacherName = c.String(nullable: false, maxLength: 8000, unicode: false),
                        TeacherAddress = c.String(nullable: false, unicode: false, storeType: "text"),
                        TeacherEmail = c.String(nullable: false),
                        TeacherContactNo = c.String(nullable: false, maxLength: 8000, unicode: false),
                        DesignationId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        CreditToBeTaken = c.Double(nullable: false),
                        TeacherRemainingCredit = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: false)
                .Index(t => t.DesignationId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        DesignationId = c.Int(nullable: false, identity: true),
                        DesignationName = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.DesignationId);
            
            CreateTable(
                "dbo.EnrollInACourses",
                c => new
                    {
                        EnrollInACourseId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Status = c.String(),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.EnrollInACourseId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: false)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false, maxLength: 50, unicode: false),
                        StudentEmail = c.String(nullable: false, maxLength: 50),
                        StudentContactNo = c.String(nullable: false, maxLength: 50, unicode: false),
                        Date = c.String(nullable: false, maxLength: 50, unicode: false),
                        StudentAddress = c.String(nullable: false),
                        StudentRegNo = c.String(maxLength: 50, unicode: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        GradeLetter = c.String(),
                    })
                .PrimaryKey(t => t.GradeId);
            
            CreateTable(
                "dbo.SaveStudentResults",
                c => new
                    {
                        SaveStudentResultId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        GradeId = c.Int(nullable: false),
                        Status = c.String(),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.SaveStudentResultId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.Grades", t => t.GradeId, cascadeDelete: false)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: false)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId)
                .Index(t => t.GradeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaveStudentResults", "StudentId", "dbo.Students");
            DropForeignKey("dbo.SaveStudentResults", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.SaveStudentResults", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.EnrollInACourses", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.EnrollInACourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseAssignToTeachers", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Teachers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.CourseAssignToTeachers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.CourseAssignToTeachers", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.ClassRoomAllocations", "DayId", "dbo.SevenDayWeeks");
            DropForeignKey("dbo.ClassRoomAllocations", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.ClassRoomAllocations", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ClassRoomAllocations", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Courses", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.SaveStudentResults", new[] { "GradeId" });
            DropIndex("dbo.SaveStudentResults", new[] { "CourseId" });
            DropIndex("dbo.SaveStudentResults", new[] { "StudentId" });
            DropIndex("dbo.Students", new[] { "DepartmentId" });
            DropIndex("dbo.EnrollInACourses", new[] { "CourseId" });
            DropIndex("dbo.EnrollInACourses", new[] { "StudentId" });
            DropIndex("dbo.Teachers", new[] { "DepartmentId" });
            DropIndex("dbo.Teachers", new[] { "DesignationId" });
            DropIndex("dbo.CourseAssignToTeachers", new[] { "CourseId" });
            DropIndex("dbo.CourseAssignToTeachers", new[] { "TeacherId" });
            DropIndex("dbo.CourseAssignToTeachers", new[] { "DepartmentId" });
            DropIndex("dbo.Courses", new[] { "SemesterId" });
            DropIndex("dbo.Courses", new[] { "DepartmentId" });
            DropIndex("dbo.ClassRoomAllocations", new[] { "DayId" });
            DropIndex("dbo.ClassRoomAllocations", new[] { "RoomId" });
            DropIndex("dbo.ClassRoomAllocations", new[] { "CourseId" });
            DropIndex("dbo.ClassRoomAllocations", new[] { "DepartmentId" });
            DropTable("dbo.SaveStudentResults");
            DropTable("dbo.Grades");
            DropTable("dbo.Students");
            DropTable("dbo.EnrollInACourses");
            DropTable("dbo.Designations");
            DropTable("dbo.Teachers");
            DropTable("dbo.CourseAssignToTeachers");
            DropTable("dbo.SevenDayWeeks");
            DropTable("dbo.Rooms");
            DropTable("dbo.Semesters");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
            DropTable("dbo.ClassRoomAllocations");
        }
    }
}
