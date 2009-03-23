load "#{RAILS_ROOT}/Rakefile"
module ScieloAdmin
# This class contains methods and variables used by the ScieloAdminController  
  
  def re_migrate(option)
    # disconnect from database temporarily while processing changes
    #ActiveRecord::Base.remove_connection
    if option.eql? "erase_migrate"
      Rake::Task["scielo:migrator:run"].invoke
    else
      Rake::Task["scielo:migrator:run"].invoke("--only-new")
    end
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