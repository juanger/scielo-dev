require File.dirname(__FILE__) + '/../test_helper'
require 'alternate_titles_controller'

# Re-raise errors caught by the controller.
class AlternateTitlesController; def rescue_action(e) raise e end; end

class AlternateTitlesControllerTest < Test::Unit::TestCase
  fixtures :languages, :journals, :journal_issues, :articles, :alternate_titles, :users 

  def setup
    @controller = AlternateTitlesController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new
    @first_id = alternate_titles(:alternate1).id
    login_as(:quentin)
  end

  def test_index
    get :index
    assert_response :success
    assert_template 'list'
  end

  def test_list
    get :list

    assert_response :success
    assert_template 'list'

    assert_not_nil assigns(:collection)
  end

  def test_show
    get :show, :id => @first_id

    assert_response :success
    assert_template 'show'

    assert_not_nil assigns(:record)
    assert assigns(:record).valid?
  end

  def test_new
    get :new

    assert_response :success
    assert_template 'new'

    assert_not_nil assigns(:record)
  end

  def test_create
    num_alternate_titles = AlternateTitle.count
    post :create, :record => {:id => 4, 
                              :title => 'Effet indirect à l\'exposition prolongée à un affichage à cristaux liquides de moniteur', 
                              :language_id => 49, 
                              :article_id => 3}
    
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_alternate_titles + 1, AlternateTitle.count
  end

  def test_edit
    get :edit, :id => @first_id

    assert_response :success
    assert_template 'edit'

    assert_not_nil assigns(:record)
    assert assigns(:record).valid?
  end

  def test_update
    post :update, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'show', :id => @first_id
  end

  def test_destroy
    assert_nothing_raised {
      AlternateTitle.find(@first_id)
    }
    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      AlternateTitle.find(@first_id)
    }
  end
end
