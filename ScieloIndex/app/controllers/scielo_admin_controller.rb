require 'scieloadmin'

class ScieloAdminController < ApplicationController
  include ScieloAdmin
  layout "admin_layout"
  
  before_filter :login_required
  
  def index
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
    logged_in? || request.host == "localhost" # || request.remote_ip == Oralia's IP
  end
  
end
