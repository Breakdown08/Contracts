using Dapper;
using System.Data;
using ContractsApi.Interfaces;
using ContractsApi.Models;

namespace ContractsApi.Services
{
    public class OracleService : IContracts
    {
        private readonly ConnectionFactory _factory;
        public OracleService(ConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<int?> GetStudyplanID(int iddisciplinename, int year, int idspeciality)
        {
            try
            {
                int educationForm = (await GetEducationForms(iddisciplinename, year, idspeciality)).Min();

                using IDbConnection dbcon = _factory.CreateConnection();
                var getStudyPlanID =
                    @"SELECT DISTINCT SP.ID_STUDYPLAN
                    FROM PORTAL.API_STUDYPLAN SP
                    WHERE SP.IDDISCIPLINENAME = :discipline
                    AND SP.YEAR = :year
                    AND SP.IDSPECIALITY = :speciality
                    AND SP.IDEDUCATION2 = :educationform
                    AND SP.MOUNT_OF_YEAR = 
                        (SELECT MAX(SP.MOUNT_OF_YEAR)
                        FROM PORTAL.API_STUDYPLAN SP
                        WHERE SP.IDDISCIPLINENAME = :discipline
                        AND SP.YEAR = :year
                        AND SP.IDSPECIALITY = :speciality
                        AND SP.IDEDUCATION2 = :educationform)";
                return await dbcon.QueryFirstAsync<int?>(getStudyPlanID, new
                {
                    discipline = iddisciplinename,
                    year = year,
                    speciality = idspeciality,
                    educationform = educationForm
                });
            }
            catch (Exception)
            {
                return null;
            }
        }
       
        public async Task<IEnumerable<ControlPointModel>> GetControlPoints(int idWorkProgram)
        {
            try
            {
                using IDbConnection dbcon = _factory.CreateConnection();
                var getControlPoints =
                    @"SELECT P.CONTROLPOINT_ID IdControlPoint,
                    P.IDEDUCATIONFORM IdEducationForm,
                    P.CONTROLPOINTSTYPE_ID IdControlPointType,
                    T.CONTROLPOINTTYPE ControlPointType,
                    P.SEMESTER Semester
                    FROM RPD.CONTROLPOINTS P
                    JOIN RPD.CONTROLPOINTTYPES T ON T.CONTROLPOINTTYPE_ID = P.CONTROLPOINTSTYPE_ID
                    WHERE P.WORKPROGRAM_ID = :idworkprogram
                    ORDER BY P.IDEDUCATIONFORM, P.CONTROLPOINTSTYPE_ID";
                return await dbcon.QueryAsync<ControlPointModel>(getControlPoints, new { idworkprogram = idWorkProgram });
            }
            catch (Exception)
            {
                return null;
            }
        }


        // SOURCES
        public async Task<IEnumerable<SourceModel>> GetSources(int idworkprogram)
        {
            try
            {
                using (IDbConnection dbcon = _factory.CreateConnection())
                {
                    var sql = @"SELECT S.Source_ID IdSource,
                                S.Source,
                                S.Position,
                                T.SOURCETYPE_ID,
                                T.SOURCETYPE
                                FROM RPD.SOURCES S
                                JOIN RPD.SOURCETYPES T ON T.SOURCETYPE_ID = S.SOURCETYPE_ID
                                WHERE S.WORKPROGRAM_ID = :wp";
                    var result = await dbcon.QueryAsync<SourceModel>(sql, new { wp = idworkprogram });
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> DeleteSource(int IdSource)
        {
            try
            {
                using (IDbConnection dbcon = _factory.CreateConnection())
                {
                    var sql = @"DELETE FROM RPD.SOURCES
                                WHERE SOURCE_ID = :pidsource";
                    int result = await dbcon.ExecuteAsync(sql, new { pidsource = IdSource });
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<SourceModel> ChangeSource(int idsource, UpdateSourceModel newSource)
        {
            try
            {
                string source = newSource.Source;
                int idsourcetype = newSource.IdSourceType;

                using IDbConnection dbcon = _factory.CreateConnection();
                var updateSource =
                    @"UPDATE RPD.SOURCES
                    SET SOURCE = :source, SOURCETYPE_ID = :idtype
                    WHERE SOURCE_ID = :id";
                int result = await dbcon.ExecuteAsync(updateSource, new { id = idsource, source = source, idtype = idsourcetype });

                return await GetSource(idsource: idsource);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<SourceModel> GetSource(int idworkprogram = -1, string source = null, int idsourcetype = -1, int? idsource = null)
        {
            try
            {
                using IDbConnection dbcon = _factory.CreateConnection();
                SourceModel source1 = null;
                if (idsource == null)
                {
                    var getSource =
                        @"SELECT
                            S.Source_ID IdSource,
                            S.Source,
                            S.Position,
                            T.SOURCETYPE_ID,
                            T.SOURCETYPE
                        FROM RPD.SOURCES S
                        JOIN RPD.SOURCETYPES T ON T.SOURCETYPE_ID = S.SOURCETYPE_ID
                        WHERE
                            WORKPROGRAM_ID = :wp
                            AND SOURCE = :s
                            AND SOURCETYPE_ID = :type";
                    source1 = await dbcon.QueryFirstAsync<SourceModel>(getSource, new { wp = idworkprogram, s = source, type = idsourcetype });
                }
                else
                {
                    var getSource =
                        @"SELECT
                            S.Source_ID IdSource,
                            S.Source,
                            S.Position,
                            T.SOURCETYPE_ID,
                            T.SOURCETYPE
                        FROM RPD.SOURCES S
                        JOIN RPD.SOURCETYPES T ON T.SOURCETYPE_ID = S.SOURCETYPE_ID
                        WHERE S.Source_ID = :source";
                    source1 = await dbcon.QueryFirstAsync<SourceModel>(getSource, new { source = idsource });
                }

                return source1;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<SourceModel> CreateSource(NewSourceModel newSource)
        {
            try
            {
                int idworkprogram = newSource.IdWorkProgram;
                string source = newSource.Source;
                int idsourcetype = newSource.IdSourceType;

                using IDbConnection dbcon = _factory.CreateConnection();
                // TODO: сделать поверку на существование такого источника до создания нового

                var insertSource = @"INSERT INTO RPD.SOURCES (WORKPROGRAM_ID, SOURCE, POSITION, SOURCETYPE_ID) VALUES(:wp, :source, :pos, :type)";
                int result = await dbcon.ExecuteAsync(insertSource, new { wp = idworkprogram, source = source, pos = -1, type = idsourcetype });
                return await GetSource(idworkprogram, source, idsourcetype);
                //var getSource = @"SELECT SOURCE_ID IDSOURCE, SOURCE, POSITION FROM SOURCES WHERE WORKPROGRAM_ID = :wp AND SOURCE = :source AND POSITION = :pos";
                //return await dbcon.QueryFirstAsync<SourceModel>(getSource, new { wp = idworkprogram, source = source, pos = -1 });
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteSourceType(int idSourceType)
        {
            try
            {
                using IDbConnection dbcon = _factory.CreateConnection();
                var deleteSourceType =
                    @"DELETE FROM RPD.SOURCETYPES WHERE SOURCETYPE_ID = :type";
                int result = await dbcon.ExecuteAsync(deleteSourceType, new { type = idSourceType });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // OBJECTIVES
        public async Task<IEnumerable<ObjectiveModel>> GetObjectives(int IdWorkProgram)
        {
            try
            {
                using (IDbConnection dbcon = _factory.CreateConnection())
                {
                    var sql = @"SELECT OBJECTIVE_ID IDOBJECTIVE, OBJECTIVE, POSITION
                                FROM OBJECTIVES
                                WHERE WORKPROGRAM_ID = :pworkprogramid";
                    var result = await dbcon.QueryAsync<ObjectiveModel>(sql, new { pworkprogramid = IdWorkProgram });
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<ObjectiveModel> CreateObjective(NewObjectiveModel newObjective)
        {
            try
            {
                int idworkprogram = newObjective.IdWorkProgram;
                string objective = newObjective.Objective;

                using (IDbConnection dbcon = _factory.CreateConnection())
                {
                    var insertObjective = @"INSERT INTO RPD.OBJECTIVES (WORKPROGRAM_ID, OBJECTIVE, POSITION) VALUES(:wp, :o, :pos)";
                    var result = await dbcon.ExecuteAsync(insertObjective, new { wp = idworkprogram, o = objective, pos = -1 });

                    var getObjective = @"SELECT OBJECTIVE_ID IDOBJECTIVE, OBJECTIVE, POSITION FROM OBJECTIVES WHERE OBJECTIVE_ID = 
                                        (SELECT MAX(OBJECTIVE_ID) FROM OBJECTIVES WHERE WORKPROGRAM_ID = :wp AND OBJECTIVE = :o AND POSITION = :pos)";
                    return await dbcon.QueryFirstAsync<ObjectiveModel>(getObjective, new { wp = idworkprogram, o = objective, pos = -1 });
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> DeleteObjective(int IdObjective)
        {
            try
            {
                using (IDbConnection dbcon = _factory.CreateConnection())
                {
                    var sql = @"DELETE FROM RPD.OBJECTIVES WHERE OBJECTIVE_ID = :id";
                    var result = await dbcon.ExecuteAsync(sql, new { id = IdObjective });
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}