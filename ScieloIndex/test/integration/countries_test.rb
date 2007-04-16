require "#{File.dirname(__FILE__)}/../test_helper"

class CountriesTest < ActionController::IntegrationTest
  fixtures :countries
  def setup
    @countries = [:mexico, :brasil]
  end

   def test_getting_index
     get "/countries"
     assert_equal 200, status
     assert_equal '/countries', path
   end

   def  test_creating_new_country
     post "countries/create", :country =>  {:id => 156, :name => 'China', :code => 'CN'}
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

   def  test_updating_name
     @countries.each { | country |
       post "/countries/update", :id => countries(country).id, :name => countries(country).name.reverse
       assert 302, status
       follow_redirect!
       assert "/countries/show/#{countries(country).id}", path
     }
   end

#    def  test_deleting
#      @countries.each { | country |
#        get "/countries/delete", :id => countries(country).id
#        puts status
#        puts path

#      }
#    end
end
