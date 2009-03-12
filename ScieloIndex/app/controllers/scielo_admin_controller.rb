require 'scieloadmin'

class ScieloAdminController < ApplicationController
  include ScieloAdmin
  layout "admin_layout"
  
  before_filter :authorized?
  
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
      re_migrate
      sts=get_statistics
      render :update do |page|
        page.replace_html "statistics", :partial => "statistics", :locals => {:statistics => sts}
        page.visual_effect :slide_down, "statistics"
      end
  end
  
  private
  
  def authorized?
    unless request.host == "localhost" # || request.remote_ip == Oralia's IP || user has logged in
      flash[:notice] = "You are not allowed to visit the requested page!! <br/> #{request.request_uri}" 
      redirect_to :controller => :citation_index, :action => :index
    end
  end
  
end
