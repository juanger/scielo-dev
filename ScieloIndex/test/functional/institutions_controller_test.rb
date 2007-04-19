require File.dirname(__FILE__) + '/../test_helper'
require 'institutions_controller'

# Re-raise errors caught by the controller.
class InstitutionsController; def rescue_action(e) raise e end; end

class InstitutionsControllerTest < Test::Unit::TestCase
  fixtures :institutions

  def setup
    @controller = InstitutionsController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = institutions(:first).id
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

    assert_not_nil assigns(:institutions)
  end

  def test_show
    get :show, :id => @first_id

    assert_response :success
    assert_template 'show'

    assert_not_nil assigns(:institution)
    assert assigns(:institution).valid?
  end

  def test_new
    get :new

    assert_response :success
    assert_template 'new'

    assert_not_nil assigns(:institution)
  end

  def test_create
    num_institutions = Institution.count

    post :create, :institution => {}

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_institutions + 1, Institution.count
  end

  def test_edit
    get :edit, :id => @first_id

    assert_response :success
    assert_template 'edit'

    assert_not_nil assigns(:institution)
    assert assigns(:institution).valid?
  end

  def test_update
    post :update, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'show', :id => @first_id
  end

  def test_destroy
    assert_nothing_raised {
      Institution.find(@first_id)
    }

    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      Institution.find(@first_id)
    }
  end
end
