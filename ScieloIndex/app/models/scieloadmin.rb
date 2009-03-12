load "#{RAILS_ROOT}/Rakefile"
module ScieloAdmin
# This class contains methods and variables used by the ScieloAdminController  
  
  def results_to_eval  
    "{_('Articles') => Article.count, _('Authors') => Author.count, _('Subjects') => Subject.count,
      _('Institutions') => Institution.count, _('Journals') => Journal.count, _('Citations') => Citation.count, 
      _('Publishers') => Publisher.count, _('Keywords') => Keyword.count, _('Collections') => Collection.count}"
  end
  
  def article_subject_results
    subjects = Subject.all
    subject_histogram=subjects.inject({}) do |result,subject|
      result[_("#{subject.name}")] = 0 if !result.key?(subject.name)
      result[_("#{subject.name}")] += subject.articles.count 
      result
    end
    subject_histogram
  end
  
  def author_institutions_results
    institutions = Institution.all
    institution_histogram=institutions.inject({}) do |result,institution|
      result[_("#{institution.name}")] = 0 if !result.key?(institution.name)
      result[_("#{institution.name}")] += institution.authors.count 
      result
    end
    institution_histogram
  end
  
  def re_migrate
    # disconnect from database temporarily while processing changes
    ActiveRecord::Base.remove_connection
    Rake::Task["scielo:migrator:run"].invoke
    ActiveRecord::Base.establish_connection
    
    Rake::Task["scielo:migrator:run"].reenable
    
    Rake::Task["db:drop"].reenable
    Rake::Task["db:create"].reenable
    Rake::Task["db:migrate"].reenable
  end
  
  def get_statistics
    f = File.open(File.join "#{RAILS_ROOT}", "tools", "Migrator", "migrator-stats")
    f.read.split("\n")
  end
  
end