require File.dirname(__FILE__) + '/../test_helper'
require 'alternate_titles_controller'

# Re-raise errors caught by the controller.
class AlternateTitlesController; def rescue_action(e) raise e end; end

class AlternateTitlesControllerTest < Test::Unit::TestCase
  fixtures :alternate_titles

  def setup
    @controller = AlternateTitlesController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = alternate_titles(:first).id
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

    assert_not_nil assigns(:alternate_titles)
  end

  def test_show
    get :show, :id => @first_id

    assert_response :success
    assert_template 'show'

    assert_not_nil assigns(:alternate_title)
    assert assigns(:alternate_title).valid?
  end

  def test_new
    get :new

    assert_response :success
    assert_template 'new'

    assert_not_nil assigns(:alternate_title)
  end

  def test_create
    num_alternate_titles = AlternateTitle.count

    post :create, :alternate_title => {}

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_alternate_titles + 1, AlternateTitle.count
  end

  def test_edit
    get :edit, :id => @first_id

    assert_response :success
    assert_template 'edit'

    assert_not_nil assigns(:alternate_title)
    assert assigns(:alternate_title).valid?
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
