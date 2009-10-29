namespace :scielo do
  
  namespace :migrator do
    
    desc "Runs the scielo migrator."
    task :run, [:opts] => :environment do |t,args|
      if !(args.opts =~ /-o|--only-new/) && !(args.opts =~ /-h|--help/)
        Rake::Task["scielo:db:clean"].execute
      end
      
      sh "./tools/Migrator/migrator.rb #{args.opts}"
    end
  end
  
  namespace :db do
    
    desc "Cleans the database and leaves it with only the users and catalogs"
    task :clean => :environment do
      # Migrate to version create_users
      # This will preserve the catalogs and users
      ENV['VERSION'] = '20090306235900'
      Rake::Task["db:migrate"].execute
      # Migrate to newest version
      ENV.delete 'VERSION'
      Rake::Task["db:migrate"].execute
    end
  end
end