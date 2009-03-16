namespace :scielo do
  
  namespace :migrator do
    
    desc "Runs the scielo migrator."
    task :run, [:opts] => :environment do |t,args|
      # Migrate to version create_users
      # This will preserve the catalogs and users
      ENV['VERSION'] = '20090306235900'
      Rake::Task["db:migrate"].execute
      # Migrate to newest version
      ENV.delete 'VERSION'
      Rake::Task["db:migrate"].execute
      sh "./tools/Migrator/migrator.rb #{args.opts}" 
    end
    
  end
end