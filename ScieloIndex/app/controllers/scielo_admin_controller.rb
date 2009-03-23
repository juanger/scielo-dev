require 'scieloadmin'

class ScieloAdminController < ApplicationController
  include ScieloAdmin
  layout "admin_layout"
  
  before_filter :login_required
  
  def index
  end
  
  def db_reinitialize
      option = params[:option]
      re_migrate(option)
      sts=get_statistics
      render :update do |page|
        page.replace_html "statistics", :partial => "statistics", :locals => {:statistics => sts}
        page.visual_effect :slide_down, "statistics"
      end
  end
  
  def download_report_file
      send_file "#{RAILS_ROOT}/tools/Migrator/migrator-stats"
  end
  
  private
  
  def authorized?
    logged_in? || request.host == "localhost" # || request.remote_ip == Oralia's IP
  end
  
end
