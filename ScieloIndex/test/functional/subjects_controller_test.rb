require File.dirname(__FILE__) + '/../test_helper'
require 'subjects_controller'

# Re-raise errors caught by the controller.
class SubjectsController; def rescue_action(e) raise e end; end

class SubjectsControllerTest < Test::Unit::TestCase
  fixtures :subjects, :users

  def setup
    @controller = SubjectsController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = subjects(:fisica).id
    @mysubject = {:parent_id => 1, :name => 'Mecanica Cuantica'}
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
    num_subjects = Subject.count

    post :create, :record => @mysubject

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_subjects + 1, Subject.count
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
      Subject.find(@first_id)
    }

    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      Subject.find(@first_id)
    }
  end
end
