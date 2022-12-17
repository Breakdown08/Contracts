using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ContractsApi.Interfaces;
using ContractsApi.Models;

namespace ContractsApi.Services
{
    public class DbService : IContracts
    {
        private readonly ConnectionFactory _factory;
        public DbService(ConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<StudentModel>> GetStudentData()
        {
            try
            {
                using IDbConnection dbcon = _factory.CreateConnection();
                var items =
                    @"SELECT 
                    'Мищенко' LastName,
                    'Кирилл' FistName,
                    'Александрович' Patronymic,
                    'Алтайский край, г. Рубцовск, ул. Пролетарская 418-97' Address,
                    'mr-kirya98@inbox.ru' Email,
                    '8-906-922-12-24' PhoneNumber,
                    'asdasdasd' Passport,
                    'asdsdasaddsaasd' INN";
                return (IEnumerable<StudentModel>)await dbcon.QueryFirstAsync<StudentModel>(items);
            }
            catch (Exception)
            {
                return null;
            }
        }





        //public async Task<int?> GetStudyplanID(int iddisciplinename, int year, int idspeciality)
        //{
        //    try
        //    {
        //        int educationForm = (await GetEducationForms(iddisciplinename, year, idspeciality)).Min();

        //        using IDbConnection dbcon = _factory.CreateConnection();
        //        var getStudyPlanID =
        //            @"SELECT DISTINCT SP.ID_STUDYPLAN
        //            FROM PORTAL.API_STUDYPLAN SP
        //            WHERE SP.IDDISCIPLINENAME = :discipline
        //            AND SP.YEAR = :year
        //            AND SP.IDSPECIALITY = :speciality
        //            AND SP.IDEDUCATION2 = :educationform
        //            AND SP.MOUNT_OF_YEAR = 
        //                (SELECT MAX(SP.MOUNT_OF_YEAR)
        //                FROM PORTAL.API_STUDYPLAN SP
        //                WHERE SP.IDDISCIPLINENAME = :discipline
        //                AND SP.YEAR = :year
        //                AND SP.IDSPECIALITY = :speciality
        //                AND SP.IDEDUCATION2 = :educationform)";
        //        return await dbcon.QueryFirstAsync<int?>(getStudyPlanID, new { 
        //            discipline = iddisciplinename, year = year, speciality = idspeciality, educationform = educationForm });
        //    }
        //    catch (Exception)
        //    {
        //        return null; 
        //    }
        //}
        //public async Task<IEnumerable<int>> GetEducationForms(int iddisciplinename, int year, int idspeciality)
        //{
        //    try
        //    {
        //        using IDbConnection dbcon = _factory.CreateConnection();
        //        var getEducationForms =
        //            @"SELECT DISTINCT SP.IDEDUCATION2
        //            FROM PORTAL.API_STUDYPLAN SP
        //            WHERE SP.YEAR = :year
        //            AND SP.IDDISCIPLINENAME = :discipline
        //            AND SP.IDSPECIALITY = :speciality
        //            ORDER BY SP.IDEDUCATION2";
        //        return await dbcon.QueryAsync<int>(getEducationForms, new {
        //            year = year, discipline = iddisciplinename, speciality = idspeciality });
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        //public async Task<IEnumerable<int>> GetDurations(int idstudyplan)
        //{
        //    try
        //    {
        //        using IDbConnection dbcon = _factory.CreateConnection();
        //        var getDuration =
        //            @"SELECT DISTINCT API.MOUNT_OF_YEAR
        //            FROM PORTAL.API_STUDYPLAN API
        //            JOIN (
        //                SELECT API.IDSPECIALITY, API.YEAR, API.IDEDUCATION2
        //                FROM PORTAL.API_STUDYPLAN API
        //                WHERE API.ID_STUDYPLAN = :pidstudyplan
        //            ) API2 ON API2.IDSPECIALITY = API.IDSPECIALITY AND API2.YEAR = API.YEAR AND API2.IDEDUCATION2 = API.IDEDUCATION2";
        //        return await dbcon.QueryAsync<int>(getDuration, new {
        //            pidstudyplan = idstudyplan });
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        //public async Task<int?> CreateWorkProgram(int iddisciplinename, int year, int idspeciality)
        //{
        //    try
        //    {
        //        using IDbConnection dbcon = _factory.CreateConnection();
        //        int? idstudyplan = await GetStudyplanID(iddisciplinename, year, idspeciality);
        //        if (idstudyplan != null)
        //        {
        //            var insertWorkProgram =
        //                @"INSERT INTO RPD.WORKPROGRAMS

        //                SELECT DISTINCT    
        //                NULL WORKPROGRAM_ID,
        //                SP.IDDISCIPLINENAME,
        //                API.YEAR,
        //                API.IDSPECIALITY,
        //                SP.TOTAL TOTALHOURS,
        //                nvl(SP.TOTAL2, 0) ZE,
        //                SP.IDCHAIR,
        //                API.IDEDUCATIONTYPE
        //                FROM CDB_DAT_STUDY_PROCESS.STUDYPLANS SP
        //                JOIN PORTAL.API_STUDYPLAN API ON API.ID_STUDYPLAN = SP.ID_STUDYPLAN
        //                WHERE SP.ID_STUDYPLAN = :studyplan";
        //            var result = await dbcon.ExecuteAsync(insertWorkProgram, new { studyplan = idstudyplan });
        //            var getWorkprogramID =
        //                @"SELECT MAX(WORKPROGRAM_ID)
        //                FROM WORKPROGRAMS
        //                WHERE
        //                    IDDISCIPLINENAME = :discipline
        //                    AND YEAR = :year
        //                    AND IDSPECIALITY = :speciality";
        //            int idWorkProgram = await dbcon.QueryFirstAsync<int>(getWorkprogramID,
        //                new { discipline = iddisciplinename, year = year, speciality = idspeciality });

        //            bool isControlPointsGenerated = await GenerateControlPoints(iddisciplinename, year, idspeciality);
        //            bool isHoursGenerated = await GenerateHours(iddisciplinename, year, idspeciality);

        //            return idWorkProgram;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        //public async Task<WorkProgramModel> GetWorkProgram(int iddisciplinename, int year, int idspeciality)
        //{
        //    try
        //    {
        //        using (IDbConnection dbcon = _factory.CreateConnection())
        //        {
        //            try
        //            {
        //                var getWorkProgram =
        //                    @"SELECT DISTINCT
        //                    WORKPROGRAM_ID idWorkProgram,
        //                    WP.IdDisciplineName,
        //                    WP.Year,
        //                    WP.IdSpeciality,
        //                    TotalHours,
        //                    ZE,
        //                    IdChair,
        //                    C.НАИМЕНОВАНИЕСКУД ChairName,
        //                    WP.IDEDUCATIONTYPE
        //                    FROM RPD.WORKPROGRAMS WP
        //                    JOIN CDB_COM_DIRECTORIES.CHAIRS C ON C.ID_CHAIR = WP.IDCHAIR
        //                    JOIN PORTAL.API_STUDYPLAN API ON API.IDDISCIPLINENAME = WP.IDDISCIPLINENAME AND API.YEAR = WP.YEAR AND API.IDSPECIALITY = WP.IDSPECIALITY
        //                    WHERE
        //                        WP.IDDISCIPLINENAME = :discipline
        //                        AND WP.YEAR = :year
        //                        AND WP.IDSPECIALITY = :speciality";
        //                return await dbcon.QueryFirstAsync<WorkProgramModel>(getWorkProgram, new { discipline = iddisciplinename, year = year, speciality = idspeciality });
        //            }
        //            catch (InvalidOperationException)
        //            {
        //                int? idWorkProgram = await CreateWorkProgram(iddisciplinename, year, idspeciality);
        //                if (idWorkProgram != null)
        //                {
        //                    return await GetWorkProgram(iddisciplinename, year, idspeciality);
        //                }
        //                else
        //                {
        //                    return null;
        //                }
        //            }


        //            /*
        //                CASE
        //                    WHEN API.IDEDUCATIONTYPE = 1 THEN 'бакалавриат'
        //                    WHEN API.IDEDUCATIONTYPE = 2 THEN 'базовый'
        //                END PROGRAMLEVEL,


        //                CASE
        //                    WHEN API.IDEDUCATIONTYPE = 1 THEN ''
        //                    WHEN API.IDEDUCATIONTYPE = 2 THEN ''
        //                        -- WHEN API.IDEDUCATIONTYPE = 2 AND API.MOUNT_OF_YEAR = :pmaxduration THEN 'среднее общее образование'
        //                        -- WHEN API.IDEDUCATIONTYPE = 2 AND API.MOUNT_OF_YEAR <> :pmaxduration THEN 'основное общее образование'
        //                END EDUCATIONLEVEL,
        //            */
        //        }
        //    }
        //    catch (Exception) 
        //    {
        //        return null; 
        //    }
        //}


        //// HOURS
        //public async Task<bool> GenerateHours(int iddisciplinename, int year, int idspeciality)
        //{
        //    try
        //    {
        //        using IDbConnection dbcon = _factory.CreateConnection();
        //        var getHours =
        //            @"SELECT DISTINCT
        //                MAX(SP.LECTURES + SP.SEMINARS + SP.LABWORKS) AUDITORHOURS,
        //                MIN(SP.TOTAL - SP.LECTURES - SP.SEMINARS - SP.LABWORKS - NVL(CPH2.HOUR, 0)) SAMWORKHOURS,
        //                MAX(NVL(CPH2.HOUR, 0)) CONTROLHOURS,
        //                API.IDEDUCATION2 IDEDUCATIONFORM,
        //                WP.WORKPROGRAM_ID idWorkProgram
        //            FROM PORTAL.API_STUDYPLAN API
        //            JOIN CDB_DAT_STUDY_PROCESS.STUDYPLANS SP ON SP.ID_STUDYPLAN = API.ID_STUDYPLAN
        //            LEFT JOIN CDB_DAT_STUDY_PROCESS.CONTROLPOINTSHOUR2 CPH2 ON CPH2.IDEDUCATION = API.IDEDUCATION2 AND API.IDEDUCATIONTYPE = 1
        //            JOIN RPD.WORKPROGRAMS WP ON WP.IDDISCIPLINENAME = API.IDDISCIPLINENAME AND WP.YEAR = API.YEAR AND WP.IDSPECIALITY = API.IDSPECIALITY
        //            WHERE
        //                API.IDDISCIPLINENAME = :discipline
        //                AND API.YEAR = :year
        //                AND API.IDSPECIALITY = :speciality
        //            GROUP BY API.IDEDUCATION2, WP.WORKPROGRAM_ID";
        //        var studyPlanHours = await dbcon.QueryAsync<StudyPlanHourModel>(getHours, new { discipline = iddisciplinename, year = year, speciality = idspeciality });

        //        foreach (var hour in studyPlanHours)
        //        {
        //            bool result;
        //            if (hour.AuditorHours != 0)
        //            {
        //                result = await CreateHour(1, hour.AuditorHours, hour.idWorkProgram, hour.IdEducationForm, 1);
        //            }
        //            if (hour.SamworkHours != 0)
        //            {
        //                result = await CreateHour(2, hour.SamworkHours, hour.idWorkProgram, hour.IdEducationForm, 2);
        //            }
        //            if (hour.ControlHours != 0)
        //            {
        //                result = await CreateHour(5, hour.ControlHours, hour.idWorkProgram, hour.IdEducationForm, 5);
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception) 
        //    {
        //        return false;
        //    }
        //}

        //public async Task<bool> CreateHour(int idhourstype, int hours, int idWorkProgram, int ideducationform, int position = -1)
        //{
        //    try
        //    {
        //        using IDbConnection dbcon = _factory.CreateConnection();
        //        var insertHours =
        //            @"INSERT INTO RPD.HOURS (WORKPROGRAM_ID, HOURSTYPE_ID, HOURS, POSITION, IDEDUCATIONFORM)
        //            VALUES (:workprogram, :hourstype, :hours, :position, :educationform)";
        //        var result = await dbcon.ExecuteAsync(insertHours,
        //            new { workprogram = idWorkProgram, hourstype = idhourstype, hours = hours, position = position, educationform = ideducationform });
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        //public async Task<IEnumerable<TotalHoursModel>> GetTotalHours(int idWorkProgram)
        //{
        //    try
        //    {
        //        using IDbConnection dbcon = _factory.CreateConnection();
        //        var getHours =
        //            @"SELECT DISTINCT
        //                MAX(SP.LECTURES) lectures,
        //                MAX(SP.SEMINARS) practical,
        //                MAX(SP.LABWORKS) laboratory,
        //                MIN(SP.TOTAL - SP.LECTURES - SP.SEMINARS - SP.LABWORKS - NVL(CPH2.HOUR, 0)) selfstudy,
        //                MAX(SP.TOTAL) TotalHours,
        //                SP.ID_STUDYPLAN,
        //                API.IDEDUCATION2 IDEDUCATIONFORM,
        //                API.ID_SPECIALITYEDUCATION IDPLAN
        //            FROM PORTAL.API_STUDYPLAN API
        //            JOIN CDB_DAT_STUDY_PROCESS.STUDYPLANS SP ON SP.ID_STUDYPLAN = API.ID_STUDYPLAN
        //            LEFT JOIN CDB_DAT_STUDY_PROCESS.CONTROLPOINTSHOUR2 CPH2 ON CPH2.IDEDUCATION = API.IDEDUCATION2 AND API.IDEDUCATIONTYPE = 1
        //            JOIN WORKPROGRAM.WORKPROGRAMS WP ON WP.IDDISCIPLINENAME = API.IDDISCIPLINENAME AND WP.YEAR = API.YEAR AND WP.IDSPECIALITY = API.IDSPECIALITY
        //            WHERE
        //                WP.ID = :workprogram
        //            GROUP BY API.IDEDUCATION2, SP.ID_STUDYPLAN, API.ID_SPECIALITYEDUCATION";
        //        return await dbcon.QueryAsync<TotalHoursModel>(getHours, new { workprogram = idWorkProgram });
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}


        //// CONTROL POINTS
        //public async Task<IEnumerable<int>> GetWorkProgramEducationForms(int idWorkProgram)
        //{
        //    try
        //    {
        //        using IDbConnection dbcon = _factory.CreateConnection();
        //        var getEducationForms =
        //            @"SELECT DISTINCT SP.IDEDUCATION2
        //            FROM PORTAL.API_STUDYPLAN SP
        //            JOIN RPD.WORKPROGRAMS WP ON WP.YEAR = SP.YEAR
        //                AND WP.IDDISCIPLINENAME = SP.IDDISCIPLINENAME
        //                AND WP.IDSPECIALITY = SP.IDSPECIALITY
        //            WHERE WP.WORKPROGRAM_ID = :id
        //            ORDER BY SP.IDEDUCATION2";
        //        return await dbcon.QueryAsync<int>(getEducationForms, new { id = idWorkProgram });
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        //public async Task<bool> GenerateControlPoints(int iddisciplinename, int year, int idspeciality)
        //{
        //    try
        //    {
        //        using IDbConnection dbcon = _factory.CreateConnection();
        //        var insertControlPoints =
        //            @"INSERT INTO RPD.CONTROLPOINTS

        //            SELECT DISTINCT
        //                WP.WORKPROGRAM_ID,
        //                CPT.CONTROLPOINTTYPE_ID,
        //                CASE
        //                    WHEN API.IDEDUCATION2 = 1 THEN ((SS.LEVEL_ - 1) * 2 + SS.SESSION_IN_LEVEL)
        //                    WHEN API.IDEDUCATION2 = 2 THEN SS.LEVEL_
        //                END SEMESTER,
        //                -1 POSITION,
        //                NULL CONTROLPOINT_ID,
        //                API.IDEDUCATION2 IDEDUCATIONFORM
        //            FROM PORTAL.API_STUDYPLAN API
        //            JOIN (
        //                SELECT DISTINCT API.IDEDUCATION2 IDEDUCATION2, MAX(API.MOUNT_OF_YEAR) MOUNT_OF_YEAR
        //                FROM PORTAL.API_STUDYPLAN API
        //                WHERE
        //                    API.IDDISCIPLINENAME = :discipline
        //                    AND API.YEAR = :year
        //                    AND API.IDSPECIALITY = :speciality
        //                GROUP BY API.IDEDUCATION2) F ON F.IDEDUCATION2 = API.IDEDUCATION2 AND F.MOUNT_OF_YEAR = API.MOUNT_OF_YEAR
        //            JOIN CDB_DAT_STUDY_PROCESS.LESSONTYPES LT ON LT.IDSTUDYPLAN = API.ID_STUDYPLAN AND LT.IDTYPE IN (5, 6, 7, 8, 23)
        //            JOIN CDB_DAT_STUDY_PROCESS.SPECIALITYSESSIONS SS ON SS.ID_SPECIALITYSESSION = LT.IDSPECIALITYSESSION
        //            JOIN RPD.CONTROLPOINTTYPES CPT ON CPT.IDDISCIPLINETYPE = LT.IDTYPE
        //            JOIN RPD.WORKPROGRAMS WP ON WP.IDDISCIPLINENAME = :discipline AND WP.YEAR = :year AND WP.IDSPECIALITY = :speciality
        //            WHERE
        //                API.IDDISCIPLINENAME = :discipline
        //                AND API.YEAR = :year
        //                AND API.IDSPECIALITY = :speciality
        //            ORDER BY IDEDUCATIONFORM, CONTROLPOINTTYPE_ID";
        //        var result = await dbcon.ExecuteAsync(insertControlPoints, new { discipline = iddisciplinename, year = year, speciality = idspeciality });
        //        return true;
        //    }
        //    catch (Exception) 
        //    {
        //        return false;
        //    }
        //}
        //public async Task<IEnumerable<ControlPointModel>> GetControlPoints(int idWorkProgram)
        //{
        //    try
        //    {
        //        using IDbConnection dbcon = _factory.CreateConnection();
        //        var getControlPoints =
        //            @"SELECT P.CONTROLPOINT_ID IdControlPoint,
        //            P.IDEDUCATIONFORM IdEducationForm,
        //            P.CONTROLPOINTSTYPE_ID IdControlPointType,
        //            T.CONTROLPOINTTYPE ControlPointType,
        //            P.SEMESTER Semester
        //            FROM RPD.CONTROLPOINTS P
        //            JOIN RPD.CONTROLPOINTTYPES T ON T.CONTROLPOINTTYPE_ID = P.CONTROLPOINTSTYPE_ID
        //            WHERE P.WORKPROGRAM_ID = :idWorkProgram
        //            ORDER BY P.IDEDUCATIONFORM, P.CONTROLPOINTSTYPE_ID";
        //        return await dbcon.QueryAsync<ControlPointModel>(getControlPoints, new { idWorkProgram = idWorkProgram });
        //        }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}


        //// SOURCES
        ////public async Task<IEnumerable<SourceModel>> GetSources(int idWorkProgram)
        ////{
        ////    try
        ////    {
        ////        using (IDbConnection dbcon = _factory.CreateConnection())
        ////        {
        ////            var sql = @"SELECT S.Source_ID IdSource,
        ////                        S.Source,
        ////                        S.Position,
        ////                        T.SOURCETYPE_ID,
        ////                        T.SOURCETYPE
        ////                        FROM RPD.SOURCES S
        ////                        JOIN RPD.SOURCETYPES T ON T.SOURCETYPE_ID = S.SOURCETYPE_ID
        ////                        WHERE S.WORKPROGRAM_ID = :wp";
        ////            var result = await dbcon.QueryAsync<SourceModel>(sql, new { wp = idWorkProgram });
        ////            return result;
        ////        }
        ////    }
        ////    catch (Exception) 
        ////    {
        ////        return null;
        ////    }
        ////}
        ////public async Task<bool> DeleteSource(int IdSource)
        ////{
        ////    try
        ////    {
        ////        using (IDbConnection dbcon = _factory.CreateConnection())
        ////        {
        ////            var sql = @"DELETE FROM RPD.SOURCES
        ////                        WHERE SOURCE_ID = :pidsource";
        ////            int result = await dbcon.ExecuteAsync(sql, new { pidsource = IdSource });
        ////            return true;
        ////        }
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return false;
        ////    }
        ////}
        ////public async Task<SourceModel> ChangeSource(int idsource, UpdateSourceModel newSource)
        ////{
        ////    try
        ////    {
        ////        string source = newSource.Source;
        ////        int idsourcetype = newSource.IdSourceType;

        ////        using IDbConnection dbcon = _factory.CreateConnection();
        ////        var updateSource = 
        ////            @"UPDATE RPD.SOURCES
        ////            SET SOURCE = :source, SOURCETYPE_ID = :idtype
        ////            WHERE SOURCE_ID = :id";
        ////        int result = await dbcon.ExecuteAsync(updateSource, new { id = idsource, source = source, idtype = idsourcetype });

        ////        return await GetSource(idsource: idsource);
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return null;
        ////    }
        ////}
        ////public async Task<SourceModel> GetSource(int idWorkProgram = -1, string source = null, int idsourcetype = -1, int? idsource = null)
        ////{
        ////    try
        ////    {
        ////        using IDbConnection dbcon = _factory.CreateConnection();
        ////        SourceModel source1 = null;
        ////        if (idsource == null)
        ////        {
        ////            var getSource =
        ////                @"SELECT
        ////                    S.Source_ID IdSource,
        ////                    S.Source,
        ////                    S.Position,
        ////                    T.SOURCETYPE_ID,
        ////                    T.SOURCETYPE
        ////                FROM RPD.SOURCES S
        ////                JOIN RPD.SOURCETYPES T ON T.SOURCETYPE_ID = S.SOURCETYPE_ID
        ////                WHERE
        ////                    WORKPROGRAM_ID = :wp
        ////                    AND SOURCE = :s
        ////                    AND SOURCETYPE_ID = :type";
        ////            source1 = await dbcon.QueryFirstAsync<SourceModel>(getSource, new { wp = idWorkProgram, s = source, type = idsourcetype });
        ////        }
        ////        else
        ////        {
        ////            var getSource =
        ////                @"SELECT
        ////                    S.Source_ID IdSource,
        ////                    S.Source,
        ////                    S.Position,
        ////                    T.SOURCETYPE_ID,
        ////                    T.SOURCETYPE
        ////                FROM RPD.SOURCES S
        ////                JOIN RPD.SOURCETYPES T ON T.SOURCETYPE_ID = S.SOURCETYPE_ID
        ////                WHERE S.Source_ID = :source";
        ////            source1 = await dbcon.QueryFirstAsync<SourceModel>(getSource, new { source = idsource });
        ////        }

        ////        return source1;
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return null;
        ////    }
        ////}
        ////public async Task<SourceModel> CreateSource(NewSourceModel newSource)
        ////{
        ////    try
        ////    {
        ////        int idWorkProgram = newSource.idWorkProgram;
        ////        string source = newSource.Source;
        ////        int idsourcetype = newSource.IdSourceType;

        ////        using IDbConnection dbcon = _factory.CreateConnection();
        ////        // TODO: сделать поверку на существование такого источника до создания нового

        ////        var insertSource = @"INSERT INTO RPD.SOURCES (WORKPROGRAM_ID, SOURCE, POSITION, SOURCETYPE_ID) VALUES(:wp, :source, :pos, :type)";
        ////        int result = await dbcon.ExecuteAsync(insertSource, new { wp = idWorkProgram, source = source, pos = -1, type = idsourcetype });
        ////        return await GetSource(idWorkProgram, source, idsourcetype);
        ////        //var getSource = @"SELECT SOURCE_ID IDSOURCE, SOURCE, POSITION FROM SOURCES WHERE WORKPROGRAM_ID = :wp AND SOURCE = :source AND POSITION = :pos";
        ////        //return await dbcon.QueryFirstAsync<SourceModel>(getSource, new { wp = idWorkProgram, source = source, pos = -1 });
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return null;
        ////    }
        ////}
        ////public async Task<IEnumerable<SourceTypeModel>> GetSourceTypes()
        ////{
        ////    try
        ////    {
        ////        using IDbConnection dbcon = _factory.CreateConnection();
        ////        var getSourceTypes =
        ////            @"SELECT SOURCETYPE_ID IDSOURCETYPE, SOURCETYPE
        ////            FROM RPD.SOURCETYPES";
        ////        return await dbcon.QueryAsync<SourceTypeModel>(getSourceTypes);
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return null;
        ////    }
        ////}
        ////public async Task<SourceTypeModel> AddSourceType(NewSourceTypeModel newSourceType)
        ////{
        ////    try
        ////    {
        ////        string sourcetype = newSourceType.SourceType;
        ////        SourceTypeModel sourceType = await GetSourceType(sourcetype: sourcetype);
        ////        if (sourceType == null)
        ////        {
        ////            // такая запись существует, контроллер должен вернуть 302
        ////            sourceType.IdSourceType = -1;
        ////            return sourceType;
        ////        }

        ////        using IDbConnection dbcon = _factory.CreateConnection();
        ////        var insertSourceType =
        ////            @"INSERT INTO RPD.SOURCETYPES (SOURCETYPE)
        ////            VALUES(:type)";
        ////        int result = await dbcon.ExecuteAsync(insertSourceType, new { type = sourcetype });
        ////        return await GetSourceType(sourcetype: sourcetype);
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return null;
        ////    }
        ////}
        ////public async Task<SourceTypeModel> GetSourceType(string sourcetype = null, int? idsourcetype = null)
        ////{
        ////    try
        ////    {
        ////        using IDbConnection dbcon = _factory.CreateConnection();
        ////        if (idsourcetype == null)
        ////        {
        ////            var getSourceType =
        ////                @"SELECT SOURCETYPE_ID IDSOURCETYPE, SOURCETYPE
        ////                FROM RPD.SOURCETYPES
        ////                WHERE SOURCETYPE = :type";
        ////            return await dbcon.QueryFirstAsync<SourceTypeModel>(getSourceType, new { type = sourcetype });
        ////        }
        ////        else
        ////        {
        ////            var getSourceType =
        ////                @"SELECT SOURCETYPE_ID IDSOURCETYPE, SOURCETYPE
        ////                FROM RPD.SOURCETYPES
        ////                WHERE SOURCETYPE_ID = :type";
        ////            return await dbcon.QueryFirstAsync<SourceTypeModel>(getSourceType, new { type = idsourcetype });
        ////        }
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return null;
        ////    }
        ////}
        ////public async Task<SourceTypeModel> UpdateSourceType(int idSourceType, UpdateSourceTypeModel updateSourceType)
        ////{
        ////    try
        ////    {
        ////        string sourcetype = updateSourceType.SourceType;

        ////        SourceTypeModel sourceType = await GetSourceType(sourcetype: sourcetype);
        ////        if (sourceType == null)
        ////        {
        ////            using IDbConnection dbcon = _factory.CreateConnection();
        ////            var updateSourceTypeSQL =
        ////                @"UPDATE RPD.SOURCETYPES
        ////                SET SOURCETYPE = :TYPE
        ////                WHERE SOURCETYPE_ID = :id";
        ////            int result = await dbcon.ExecuteAsync(updateSourceTypeSQL, new { id = idSourceType, type = sourcetype });
        ////            return await GetSourceType(idsourcetype: idSourceType);
        ////        }
        ////        else
        ////        {
        ////            // 302 Found если запись существует
        ////            sourceType.IdSourceType = -1;
        ////            return sourceType;
        ////        }
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return null;
        ////    }
        ////}
        //public async Task<bool> DeleteSourceType(int idSourceType)
        //{
        //    try
        //    {
        //        using IDbConnection dbcon = _factory.CreateConnection();
        //        var deleteSourceType =
        //            @"DELETE FROM RPD.SOURCETYPES WHERE SOURCETYPE_ID = :type";
        //        int result = await dbcon.ExecuteAsync(deleteSourceType, new { type = idSourceType });
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //// OBJECTIVES
        //public async Task<IEnumerable<ObjectiveModel>> GetObjectives(int idWorkProgram)
        //{
        //    try
        //    {
        //        using (IDbConnection dbcon = _factory.CreateConnection())
        //        {
        //            var sql = @"SELECT OBJECTIVE_ID IDOBJECTIVE, OBJECTIVE, POSITION
        //                        FROM OBJECTIVES
        //                        WHERE WORKPROGRAM_ID = :pworkprogramid";
        //            var result = await dbcon.QueryAsync<ObjectiveModel>(sql, new { pworkprogramid = idWorkProgram });
        //            return result;
        //        }
        //    }
        //    catch (Exception) 
        //    {
        //        return null; 
        //    }
        //}
        ////public async Task<ObjectiveModel> CreateObjective(NewObjectiveModel newObjective)
        ////{
        ////    try
        ////    {
        ////        int idWorkProgram = newObjective.idWorkProgram;
        ////        string objective = newObjective.Objective;

        ////        using (IDbConnection dbcon = _factory.CreateConnection())
        ////        {
        ////            var insertObjective = @"INSERT INTO RPD.OBJECTIVES (WORKPROGRAM_ID, OBJECTIVE, POSITION) VALUES(:wp, :o, :pos)";
        ////            var result = await dbcon.ExecuteAsync(insertObjective, new { wp = idWorkProgram, o = objective, pos = -1 });

        ////            var getObjective = @"SELECT OBJECTIVE_ID IDOBJECTIVE, OBJECTIVE, POSITION FROM OBJECTIVES WHERE OBJECTIVE_ID = 
        ////                                (SELECT MAX(OBJECTIVE_ID) FROM OBJECTIVES WHERE WORKPROGRAM_ID = :wp AND OBJECTIVE = :o AND POSITION = :pos)";
        ////            return await dbcon.QueryFirstAsync<ObjectiveModel>(getObjective, new { wp = idWorkProgram, o = objective, pos = -1 });
        ////        }
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return null;
        ////    }
        ////}
        //public async Task<bool> DeleteObjective(int IdObjective)
        //{
        //    try
        //    {
        //        using (IDbConnection dbcon = _factory.CreateConnection())
        //        {
        //            var sql = @"DELETE FROM RPD.OBJECTIVES WHERE OBJECTIVE_ID = :id";
        //            var result = await dbcon.ExecuteAsync(sql, new { id = IdObjective });
        //            return true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        ////public async Task<ObjectiveModel> ChangeObjective(int idobjective, UpdateObjectiveModel updateObjective)
        ////{
        ////    try
        ////    {
        ////        string objective = updateObjective.Objective;
        ////        if (objective == null || objective == "")
        ////        {
        ////            return null;
        ////        }

        ////        using IDbConnection dbcon = _factory.CreateConnection();
        ////        var updateObjectiveSQL = @"UPDATE RPD.OBJECTIVES
        ////                                 SET OBJECTIVE = :objective
        ////                                 WHERE OBJECTIVE_ID = :id";
        ////        int result = await dbcon.ExecuteAsync(updateObjectiveSQL, new { id = idobjective, objective = objective });

        ////        var getObjective = @"SELECT OBJECTIVE_ID IDOBJECTIVE, OBJECTIVE, POSITION
        ////                           FROM OBJECTIVES
        ////                           WHERE OBJECTIVE_ID = :id
        ////                           ";
        ////        return await dbcon.QueryFirstAsync<ObjectiveModel>(getObjective, new { id = idobjective });
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return null;
        ////    }
        ////}


        //// PROFILES
        //public async Task<IEnumerable<ProfileModel>> GetProfiles(int idstudyplan)
        //{
        //    try
        //    {
        //        using (IDbConnection dbcon = _factory.CreateConnection())
        //        {
        //            var sql = @"SELECT DISTINCT C.ID_CYCLE IDPROFILE, C.CYCLE AS PROFILENAME
        //                        FROM CDB_DAT_STUDY_PROCESS.STUDYPLANS SP
        //                        JOIN CDB_DAT_STUDY_PROCESS.CYCLESPECIALITYEDUCATIONS CSE ON CSE.ID_CYCLESPECIALITYEDUCATION = SP.IDCYCLESPECIALITYEDUCATION
        //                        JOIN CDB_COM_DIRECTORIES.CYCLES C ON C.ID_CYCLE = CSE.IDCYCLE
        //                        WHERE C.IDCYCLE_TYPE = 7
        //                          AND SP.ID_STUDYPLAN = :pidstudyplan";
        //            IEnumerable<ProfileModel> result = await dbcon.QueryAsync<ProfileModel>(sql, new { pidstudyplan = idstudyplan });

        //            if (result is null || result.Count() == 0)
        //            {
        //                sql = @"SELECT DISTINCT C.ID_CYCLE IDPROFILE, C.CYCLE AS PROFILENAME
        //                        FROM CDB_DAT_STUDY_PROCESS.SPECIALITYEDUCATIONS SE
        //                        JOIN CDB_DAT_STUDY_PROCESS.CYCLESPECIALITYEDUCATIONS CSE ON CSE.IDSPECIALITYEDUCATION = SE.ID_SPECIALITYEDUCATION
        //                        JOIN CDB_COM_DIRECTORIES.CYCLES C ON C.ID_CYCLE = CSE.IDCYCLE
        //                        JOIN (SELECT API.IDSPECIALITY, API.YEAR, API.IDEDUCATION2
        //                              FROM PORTAL.API_STUDYPLAN API
        //                              WHERE API.ID_STUDYPLAN = :pidstudyplan) API ON API.IDSPECIALITY = SE.IDSPECIALITY AND API.YEAR = SE.YEAR AND API.IDEDUCATION2 = SE.IDEDUCATION2
        //                        WHERE C.IDCYCLE_TYPE = 7
        //                        ORDER BY IDPROFILE";
        //                result = await dbcon.QueryAsync<ProfileModel>(sql, new { pidstudyplan = idstudyplan });
        //            }
        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        //public async Task<IEnumerable<ProfileModel>> GetProfilesByidWorkProgram(int idWorkProgram)
        //{
        //    try
        //    {
        //        int? idstudyplan = null;//await GetStudyPlanIdByWorkProgramId(idWorkProgram);
        //        if (idstudyplan == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            return await GetProfiles((int)idstudyplan);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        //public async Task<ProfileModel> GetProfileByIdProfile(int IdProfile)
        //{
        //    try
        //    {
        //        using (IDbConnection dbcon = _factory.CreateConnection())
        //        {
        //            var sql = @"SELECT DISTINCT C.ID_CYCLE IDPROFILE, C.CYCLE AS PROFILENAME
        //                        FROM CDB_COM_DIRECTORIES.CYCLES C
        //                        WHERE C.ID_CYCLE = :pidprofile";
        //            var result = await dbcon.QueryFirstAsync<ProfileModel>(sql, new { pidprofile = IdProfile });

        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}


    }
}