require File.dirname(__FILE__) + '/../test_helper'
require 'countries_controller'

# Re-raise errors caught by the controller.
class CountriesController; def rescue_action(e) raise e end; end

class CountriesControllerTest < Test::Unit::TestCase
  fixtures :countries, :publishers, :collections, :institutions, :users

  def setup
    @controller = CountriesController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = countries(:brasil).id
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
    num_countries = Country.count

    post :create, :record =>{:id => "90", :name => "Argentina", :code => "AR"}

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_countries + 1, Country.count
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
      Country.find(@first_id)
    }

    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      Country.find(@first_id)
    }
  end
end
