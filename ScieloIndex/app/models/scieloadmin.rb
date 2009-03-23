load "#{RAILS_ROOT}/Rakefile"
module ScieloAdmin
# This class contains methods and variables used by the ScieloAdminController  
  
  def re_migrate
    # disconnect from database temporarily while processing changes
    #ActiveRecord::Base.remove_connection
    Rake::Task["scielo:migrator:run"].invoke
    #ActiveRecord::Base.establish_connection
    
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