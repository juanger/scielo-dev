require 'scieloadmin'
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
  
  def db_action_to_exec
      @completition_msg=execute_rake_tasks(params[:option])
      render :action => scielo_management, :object => @completition_msg
  end
  
end
