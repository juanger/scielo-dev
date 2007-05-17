require "#{File.dirname(__FILE__)}/../test_helper"

class CountriesTest < ActionController::IntegrationTest
  fixtures :countries, :publishers, :collections, :institutions

  def setup
    @countries = [:mexico, :brasil, :usa]
  end

   def test_getting_index
     get "/countries"
     assert_equal 200, status
     assert_equal '/countries', path
   end

   def test_new
     get "/countries/new"
     assert_equal 200, status
     assert_equal '/countries/new', path
   end

   
   def  test_creating_new_country
     post "countries/create", :record =>  {:id => 156, :name => 'China', :code => 'CN'}
     assert_equal 302, status
     follow_redirect!
     assert_equal '/countries/list', path
   end

   def test_showing
     @countries.each { | country |
       post "/countries/show", :id => countries(country).id
       assert 200, status
       assert_equal '/countries/show', path
     }
   end

   def test_editing
     @countries.each { | country |
       post "/countries/edit", :id => countries(country).id
       assert 200, status
       assert "/countries/edit/#{countries(country).id}", path
     }
   end

   def test_updating_name
     @countries.each { | country |
       post "/countries/update", :id => countries(country).id, :name => countries(country).name.reverse
       assert 302, status
       follow_redirect!
       assert "/countries/show/#{countries(country).id}", path
     }
   end

    def test_deleting
     @countries.each { | country |
       get "/countries/destroy", :id => countries(country).id
       assert 302, status
       assert '/countries/list',  path
     }
   end
end
