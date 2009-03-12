require 'scieloadmin'
load "#{RAILS_ROOT}/Rakefile"

class ScieloAdminController < ApplicationController
  include ScieloAdmin
  layout "admin_layout"
  
  def index
  end
  
  def loaded_data
    @results = eval(results_to_eval)
    Rails.cache.write("results", @results)
    @articles_subjects = article_subject_results
    Rails.cache.write("article_sub_results", @articles_subjects)
    @authors_institutions = author_institutions_results
    Rails.cache.write("author_ins_results", @authors_institutions)
    
  end
  
  def generate_chart
    width=800
    case (params[:chart])
      when "general"
        @chart_heading = params[:chart].capitalize!
        @graph = Ezgraphix::Graphic.new(:w => width, :h => 300, :c_type => "col3d", :div_name => "chart")
        @graph.data = Rails.cache.fetch("results") { eval results_to_eval }
      when "articles_and_subjects"
        @chart_heading = params[:chart].capitalize.humanize
        @graph = Ezgraphix::Graphic.new(:w => width, :h => 300, :c_type => "col3d", :div_name => "chart")
        @graph.data = Rails.cache.fetch("article_sub_results") { article_subject_results }
      when "authors_and_institutions" 
        @chart_heading = params[:chart].capitalize.humanize
        @graph = Ezgraphix::Graphic.new(:w => width, :h => 300, :c_type => "col3d", :div_name => "chart")
        @graph.data = Rails.cache.fetch("author_ins_results") { author_institutions_results }   
    end 
  end  
  
  def db_reinitialize    
      # disconnect from database temporarily while processing changes
      ActiveRecord::Base.remove_connection
      Rake::Task["scielo:migrator:run"].invoke
      Rake::Task["scielo:migrator:run"].reenable
      
      Rake::Task["db:drop"].reenable
      Rake::Task["db:create"].reenable
      Rake::Task["db:migrate"].reenable
      
      ActiveRecord::Base.establish_connection
      @completition_msg= "Database succesfully re-initialized"
      redirect_to :action => "scielo_management", :object => @completition_msg
  end
  
end
