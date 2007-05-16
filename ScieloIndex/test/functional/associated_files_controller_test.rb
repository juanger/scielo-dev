require File.dirname(__FILE__) + '/../test_helper'
require 'associated_files_controller'

# Re-raise errors caught by the controller.
class AssociatedFilesController; def rescue_action(e) raise e end; end

class AssociatedFilesControllerTest < Test::Unit::TestCase
  fixtures :associated_files

  def setup
    @controller = AssociatedFilesController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = associated_files(:first).id
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

    assert_not_nil assigns(:associated_files)
  end

  def test_show
    get :show, :id => @first_id

    assert_response :success
    assert_template 'show'

    assert_not_nil assigns(:associated_file)
    assert assigns(:associated_file).valid?
  end

  def test_new
    get :new

    assert_response :success
    assert_template 'new'

    assert_not_nil assigns(:associated_file)
  end

  def test_create
    num_associated_files = AssociatedFile.count

    post :create, :associated_file => {}

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_associated_files + 1, AssociatedFile.count
  end

  def test_edit
    get :edit, :id => @first_id

    assert_response :success
    assert_template 'edit'

    assert_not_nil assigns(:associated_file)
    assert assigns(:associated_file).valid?
  end

  def test_update
    post :update, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'show', :id => @first_id
  end

  def test_destroy
    assert_nothing_raised {
      AssociatedFile.find(@first_id)
    }

    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      AssociatedFile.find(@first_id)
    }
  end
end
