require File.dirname(__FILE__) + '/../test_helper'
require 'cites_controller'

# Re-raise errors caught by the controller.
class CitesController; def rescue_action(e) raise e end; end

class CitesControllerTest < Test::Unit::TestCase
  fixtures :cites

  def setup
    @controller = CitesController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = cites(:first).id
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

    assert_not_nil assigns(:cites)
  end

  def test_show
    get :show, :id => @first_id

    assert_response :success
    assert_template 'show'

    assert_not_nil assigns(:cite)
    assert assigns(:cite).valid?
  end

  def test_new
    get :new

    assert_response :success
    assert_template 'new'

    assert_not_nil assigns(:cite)
  end

  def test_create
    num_cites = Cite.count

    post :create, :cite => {}

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_cites + 1, Cite.count
  end

  def test_edit
    get :edit, :id => @first_id

    assert_response :success
    assert_template 'edit'

    assert_not_nil assigns(:cite)
    assert assigns(:cite).valid?
  end

  def test_update
    post :update, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'show', :id => @first_id
  end

  def test_destroy
    assert_nothing_raised {
      Cite.find(@first_id)
    }

    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      Cite.find(@first_id)
    }
  end
end
