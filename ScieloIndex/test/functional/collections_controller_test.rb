require File.dirname(__FILE__) + '/../test_helper'
require 'collections_controller'

# Re-raise errors caught by the controller.
class CollectionsController; def rescue_action(e) raise e end; end

class CollectionsControllerTest < Test::Unit::TestCase
  fixtures :collections

  def setup
    @controller = CollectionsController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = collections(:first).id
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

    assert_not_nil assigns(:collections)
  end

  def test_show
    get :show, :id => @first_id

    assert_response :success
    assert_template 'show'

    assert_not_nil assigns(:collection)
    assert assigns(:collection).valid?
  end

  def test_new
    get :new

    assert_response :success
    assert_template 'new'

    assert_not_nil assigns(:collection)
  end

  def test_create
    num_collections = Collection.count

    post :create, :collection => {}

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_collections + 1, Collection.count
  end

  def test_edit
    get :edit, :id => @first_id

    assert_response :success
    assert_template 'edit'

    assert_not_nil assigns(:collection)
    assert assigns(:collection).valid?
  end

  def test_update
    post :update, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'show', :id => @first_id
  end

  def test_destroy
    assert_nothing_raised {
      Collection.find(@first_id)
    }

    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      Collection.find(@first_id)
    }
  end
end
